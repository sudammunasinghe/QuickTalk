using Dapper;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Domain.Entities;
using QuickTalk.Infrastructure.Persistence;
using QuickTalk.Infrastructure.Persistence.Sql.Helpers;

namespace QuickTalk.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ISqlQueryLoader _queryLoader;

        private readonly string _Select_UserByEmail;
        private readonly string _Insert_User;
        private readonly string _Update_User;
        private readonly string _Insert_PrivacySettings;
        public AuthRepository(IDbConnectionFactory connectionFactory, ISqlQueryLoader queryLoader)
        {
            _connectionFactory = connectionFactory;
            _queryLoader = queryLoader;
            _Select_UserByEmail = _queryLoader.Load("Auth", "Select_UserByEmail.sql");
            _Insert_User = _queryLoader.Load("Auth", "Insert_User.sql");
            _Update_User = _queryLoader.Load("Auth", "Update_User.sql");
            _Insert_PrivacySettings = _queryLoader.Load("AccountSettings", "Insert_PrivacySettings.sql");
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var db = _connectionFactory.CreateConnection();
            return await db.QueryFirstOrDefaultAsync<User>(
                _Select_UserByEmail,
                new { Email = email }
            );
        }

        public async Task<int> RegisterUserAsync(User newUser)
        {
            using var db = _connectionFactory.CreateConnection();
            db.Open();
            using var transaction = db.BeginTransaction();
            try
            {
                var userId = await db.ExecuteScalarAsync<int>(
                    _Insert_User,
                    newUser,
                    transaction
                );

                await db.ExecuteAsync(
                    _Insert_PrivacySettings,
                    new
                    {
                        UserId = userId
                    },
                    transaction
                );
                transaction.Commit();
                return userId;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task UpdateUserAsync(User updatedUser)
        {
            using var db = _connectionFactory.CreateConnection();
            await db.ExecuteAsync(
                _Update_User,
                new
                {
                    Id = updatedUser.Id,
                    PasswordHash = updatedUser.PasswordHash,
                    Otp = updatedUser.Otp,
                    OtpExpiry = updatedUser.OtpExpiry,
                    IsUsed = updatedUser.IsUsed,
                    LastModifiedDateTime = updatedUser.LastModifiedDateTime,
                    LastSeen = updatedUser.LastSeen
                }
            );
        }
    }
}

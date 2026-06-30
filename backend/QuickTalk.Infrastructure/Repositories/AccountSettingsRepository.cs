using Dapper;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Domain.Entities;
using QuickTalk.Infrastructure.Persistence;
using QuickTalk.Infrastructure.Persistence.Sql.Helpers;

namespace QuickTalk.Infrastructure.Repositories
{
    public class AccountSettingsRepository : IAccountSettingsRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ISqlQueryLoader _queryLoader;
        private readonly string _Select_UserByUserId;
        private readonly string _Update_User;
        public AccountSettingsRepository(IDbConnectionFactory connectionFactory, ISqlQueryLoader queryLoader)
        {
            _connectionFactory = connectionFactory;
            _queryLoader = queryLoader;
            _Select_UserByUserId = _queryLoader.Load("AccountSettings", "Select_UserByUserId.sql");
            _Update_User = _queryLoader.Load("AccountSettings", "Update_User.sql");
        }

        public async Task<User?> GetUserByUserIdAsync(int userId)
        {
            using var db = _connectionFactory.CreateConnection();
            return await db.QueryFirstOrDefaultAsync<User>(
                _Select_UserByUserId,
                new
                {
                    UserId = userId
                }
            );
        }

        public async Task ChangePasswordAsync(User updatedUser)
        {
            using var db = _connectionFactory.CreateConnection();
            await db.ExecuteAsync(
                _Update_User,
                new
                {
                    UserId = updatedUser.Id,
                    PasswordHash = updatedUser.PasswordHash,
                    LastModifiedDateTime = updatedUser.LastModifiedDateTime
                }
            );
        }
    }
}

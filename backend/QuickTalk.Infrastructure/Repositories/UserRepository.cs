using Dapper;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Domain.Entities;
using QuickTalk.Infrastructure.Persistence;
using QuickTalk.Infrastructure.Persistence.Sql.Helpers;

namespace QuickTalk.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ISqlQueryLoader _queryLoader;

        private readonly string _Select_UserDetails;
        private readonly string _Select_UsersToChat;

        public UserRepository(IDbConnectionFactory connectionFactory, ISqlQueryLoader queryLoader)
        {
            _connectionFactory = connectionFactory;
            _queryLoader = queryLoader;
            _Select_UserDetails = _queryLoader.Load("User", "Select_UserDetails.sql");
            _Select_UsersToChat = _queryLoader.Load("User", "Select_UsersToChat.sql");
        }

        public async Task<User?> GetUserByUserIdAsync(int userId)
        {
            using var db = _connectionFactory.CreateConnection();
            return await db.QueryFirstOrDefaultAsync<User>(
                _Select_UserDetails,
                new { UserId = userId }
            );
        }

        public async Task<IEnumerable<User>> GetPeopleToChat(int userId)
        {
            using var db = _connectionFactory.CreateConnection();
            return await db.QueryAsync<User>(
                _Select_UsersToChat,
                new { UserId = userId }
            );
        }
    }
}

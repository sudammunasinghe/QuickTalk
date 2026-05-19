using Dapper;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Domain.Entities;
using QuickTalk.Infrastructure.Persistence;
using QuickTalk.Infrastructure.Persistence.Sql.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ISqlQueryLoader _queryLoader;

        private readonly string _Select_UserDetails;
        
        public UserRepository(IDbConnectionFactory connectionFactory, ISqlQueryLoader queryLoader)
        {
            _connectionFactory = connectionFactory;
            _queryLoader = queryLoader;
            _Select_UserDetails = _queryLoader.Load("User", "Select_UserDetails.sql");
        }

        public async Task<User?> GetCurrentUser(int userId)
        {
            using var db = _connectionFactory.CreateConnection();
            return await db.QueryFirstOrDefaultAsync<User>(
                _Select_UserDetails,
                new { UserId =  userId }
            );
        }
    }
}

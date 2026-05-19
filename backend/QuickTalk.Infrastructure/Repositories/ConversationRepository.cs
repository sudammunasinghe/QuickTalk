using Dapper;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Domain.Entities;
using QuickTalk.Infrastructure.Persistence;
using QuickTalk.Infrastructure.Persistence.Sql.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Infrastructure.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ISqlQueryLoader _queryLoader;

        private readonly string _Insert_Message;
        private readonly string _Select_UserByUserId;
        
        public ConversationRepository(IDbConnectionFactory connectionFactory, ISqlQueryLoader queryLoader)
        {
            _connectionFactory = connectionFactory;
            _queryLoader = queryLoader;
            _Insert_Message = _queryLoader.Load("Conversation", "Insert_Message.sql");
            _Select_UserByUserId = _queryLoader.Load("Conversation", "Select_UserByUserId.sql");
        }

        public async Task SendMessageAsync(Message newMessage)
        {
            using var db = _connectionFactory.CreateConnection();
            await db.ExecuteAsync(
                _Insert_Message,
                newMessage
            );
        }

        public async Task<User?> GetUserByUserIdAsync(int userId)
        {
            using var db = _connectionFactory.CreateConnection();
            return await db.QueryFirstOrDefaultAsync<User>(
                _Select_UserByUserId,
                new { UserId =  userId }
            );
        }
    }
}

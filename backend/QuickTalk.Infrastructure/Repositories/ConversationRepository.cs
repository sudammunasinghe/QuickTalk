using Dapper;
using QuickTalk.Application.DTOs.Conversation;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Domain.Entities;
using QuickTalk.Infrastructure.Persistence;
using QuickTalk.Infrastructure.Persistence.Sql.Helpers;

namespace QuickTalk.Infrastructure.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ISqlQueryLoader _queryLoader;

        private readonly string _Insert_Message;
        private readonly string _Select_UserByUserId;
        private readonly string _Select_ConversationHistory;
        private readonly string _Select_Conversations;
        private readonly string _Select_UnreadMessages;
        private readonly string _Update_Message;

        public ConversationRepository(IDbConnectionFactory connectionFactory, ISqlQueryLoader queryLoader)
        {
            _connectionFactory = connectionFactory;
            _queryLoader = queryLoader;
            _Insert_Message = _queryLoader.Load("Conversation", "Insert_Message.sql");
            _Select_UserByUserId = _queryLoader.Load("Conversation", "Select_UserByUserId.sql");
            _Select_ConversationHistory = _queryLoader.Load("Conversation", "Select_ConversationHistory.sql");
            _Select_Conversations = _queryLoader.Load("Conversation", "Select_Conversations.sql");
            _Select_UnreadMessages = _queryLoader.Load("Conversation", "Select_UnreadMessages.sql");
            _Update_Message = _queryLoader.Load("Conversation", "Update_Message.sql");
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
                new { UserId = userId }
            );
        }

        public async Task<IEnumerable<Message>> GetConversationHistory(int senderId, int receiverId)
        {
            using var db = _connectionFactory.CreateConnection();
            return await db.QueryAsync<Message>(
                _Select_ConversationHistory,
                new
                {
                    SenderId = senderId,
                    ReceiverId = receiverId
                }
            );
        }

        public async Task<IEnumerable<ConversationDto>> GetConversationsAsync(int loggedUserId)
        {
            using var db = _connectionFactory.CreateConnection();
            return await db.QueryAsync<ConversationDto>(
                _Select_Conversations,
                new { LoggedUserId = loggedUserId }
            );
        }

        public async Task<List<Message>> GetUnreadMessagesAsync(int loggedUserId, int senderId)
        {
            using var db = _connectionFactory.CreateConnection();
            var unreadMessages = await db.QueryAsync<Message>(
                _Select_UnreadMessages,
                new
                {
                    LoggedUserId = loggedUserId,
                    SenderId = senderId
                }
            );
            return unreadMessages.ToList();
        }

        public async Task MarkAsReadAsync(List<Message> unreadMessages)
        {
            using var db = _connectionFactory.CreateConnection();
            await db.ExecuteAsync(
                _Update_Message,
                unreadMessages
            );
        }
    }
}

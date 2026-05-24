using QuickTalk.Application.DTOs.Conversation;
using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Interfaces.IRepositories
{
    public interface IConversationRepository
    {
        Task SendMessageAsync(Message newMessage);
        Task<User?> GetUserByUserIdAsync(int userId);
        Task<IEnumerable<Message>> GetConversationHistory(int senderId, int receiverId);
        Task<IEnumerable<ConversationDto>> GetConversationsAsync(int loggedUserId);
        Task<List<Message>> GetUnreadMessagesAsync(int loggedUserId, int senderId);
        Task MarkAsReadAsync(List<Message> unreadMessages);
    }
}

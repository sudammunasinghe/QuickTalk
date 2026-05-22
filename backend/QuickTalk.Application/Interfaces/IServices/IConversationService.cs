using QuickTalk.Application.DTOs.Conversation;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IConversationService
    {
        Task SendMessageAsync(SendMessageDto dto);
        Task<IEnumerable<CoversationHistoryDto>> GetConversationHistory(int receiverId);
        Task<IEnumerable<ConversationDto>> GetConversationsAsync();
    }
}

using QuickTalk.Application.DTOs.Conversation;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IChatNotifier
    {
        Task SendToUser(ChatMessageDto dto);
    }
}

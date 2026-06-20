using Microsoft.AspNetCore.SignalR;
using QuickTalk.Api.Hub;
using QuickTalk.Application.DTOs.Conversation;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Api.SignalR
{
    public class ChatNotifier : IChatNotifier
    {
        private readonly IHubContext<ChatHub> _hub;
        public ChatNotifier(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }
        public Task SendToUser(ChatMessageDto dto)
        {
            return _hub.Clients.User(dto.ReceiverId)
                .SendAsync("ReceiveMessage", dto.SenderId, dto.Message);
        }
    }
}

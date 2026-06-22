using QuickTalk.Application.DTOs.Conversation;
using QuickTalk.Application.Exceptions;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IChatNotifier _chatNotifier;
        public ConversationService(
            IConversationRepository conversationRepository, 
            ICurrentUser currentUser,
            IChatNotifier chatNotifier
            )
        {
            _conversationRepository = conversationRepository;
            _currentUser = currentUser;
            _chatNotifier = chatNotifier;
        }

        public async Task SendMessageAsync(SendMessageDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Message))
                throw new BadRequestException("Message cannot be empty.");

            var receiver =
                await _conversationRepository.GetUserByUserIdAsync(dto.ReceiverId);
            if (receiver == null)
                throw new NotFoundException("Receiver not found.");

            var loggedUser = _currentUser.UserId;
            if (loggedUser == dto.ReceiverId)
                throw new BadRequestException("You cannot send messages to yourself.");

            var message = new Message
            {
                SenderId = loggedUser,
                ReceiverId = dto.ReceiverId,
                MessageText = dto.Message,
                IsRead = false
            };

            //save to DB
            await _conversationRepository.SendMessageAsync(message);

            //real time update uisng SignalR
            var chatDetails = new ChatMessageDto
            {
                SenderId = loggedUser.ToString(),
                ReceiverId = message.ReceiverId.ToString(),
                Message = message.MessageText
            };
            await _chatNotifier.SendToUser(chatDetails);
        }

        public async Task<IEnumerable<CoversationHistoryDto>> GetConversationHistory(int receiverId)
        {
            var receiver =
                await _conversationRepository.GetUserByUserIdAsync(receiverId);
            if (receiver == null)
                throw new NotFoundException("Receiver not found.");

            var loggedUser = _currentUser.UserId;
            var conversation =
                await _conversationRepository.GetConversationHistory(loggedUser, receiverId);

            return conversation
                .Select(msg => new CoversationHistoryDto
                {
                    Id = msg.Id,
                    SenderId = msg.SenderId,
                    Message = msg.MessageText,
                    SendAt = msg.LastModifiedDateTime,
                    IsMine = loggedUser == msg.SenderId
                });
        }

        public async Task<IEnumerable<ConversationDto>> GetConversationsAsync()
        {
            var loggedUser = _currentUser.UserId;
            return await _conversationRepository.GetConversationsAsync(loggedUser);
        }

        public async Task MarkAsReadAsync(int senderId)
        {
            var loggedUser = _currentUser.UserId;
            var UnreadMessages =
                await _conversationRepository.GetUnreadMessagesAsync(loggedUser, senderId);

            if (!UnreadMessages.Any())
                throw new BadRequestException("No any unread messages.");

            UnreadMessages.ForEach(msg =>
            {
                msg.IsRead = true;
                msg.LastModifiedDateTime = DateTime.UtcNow;
            });
            await _conversationRepository.MarkAsReadAsync(UnreadMessages);
        }
    }
}

using QuickTalk.Application.DTOs.Conversation;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using QuickTalk.Domain.Entities;
using QuickTalk.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly ICurrentUser _currentUser;
        public ConversationService(IConversationRepository conversationRepository, ICurrentUser currentUser)
        {
            _conversationRepository = conversationRepository;
            _currentUser = currentUser;
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
            await _conversationRepository.SendMessageAsync(message);
        }
    }
}

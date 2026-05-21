using QuickTalk.Application.DTOs.Conversation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IConversationService
    {
        Task SendMessageAsync(SendMessageDto dto);
        Task<IEnumerable<CoversationHistoryDto>> GetConversationHistory(int receiverId);
    }
}

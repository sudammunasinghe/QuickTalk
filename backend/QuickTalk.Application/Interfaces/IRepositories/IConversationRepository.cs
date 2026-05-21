using QuickTalk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Interfaces.IRepositories
{
    public interface IConversationRepository
    {
        Task SendMessageAsync(Message newMessage);
        Task<User?> GetUserByUserIdAsync(int userId);
        Task<IEnumerable<Message>> GetConversationHistory(int senderId, int receiverId);
    }
}

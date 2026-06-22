using QuickTalk.Application.DTOs.Conversation;
using QuickTalk.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IChatNotifier
    {
        Task SendToUser(ChatMessageDto dto);
    }
}

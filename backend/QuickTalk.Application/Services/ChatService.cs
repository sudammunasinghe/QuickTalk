using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        public ChatService(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }
    }
}

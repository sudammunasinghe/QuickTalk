using Microsoft.AspNetCore.Mvc;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
    }
}

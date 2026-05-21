using Microsoft.AspNetCore.Mvc;
using QuickTalk.Application.DTOs.ApiResponse;
using QuickTalk.Application.DTOs.Conversation;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpPost("message")]
        public async Task<ActionResult<ApiResponse<string>>> SendMessageAsync([FromBody] SendMessageDto dto)
        {
            await _conversationService.SendMessageAsync(dto);
            return Ok(new ApiResponse<string>
            {
                IsSuccess = true,
                Message = "Message sent successfully."
            });
        }

        [HttpGet("{receiverId}/message")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CoversationHistoryDto>>>> GetConversationHistory(int receiverId)
        {
            var conversation = await _conversationService.GetConversationHistory(receiverId);
            return Ok(new ApiResponse<IEnumerable<CoversationHistoryDto>>
            {
                IsSuccess = true,
                Data = conversation,
                Message = "Conversation history retrieved successfully."
            });
        }
    }
}

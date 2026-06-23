using Microsoft.AspNetCore.Mvc;
using QuickTalk.Application.DTOs.ApiResponse;
using QuickTalk.Application.DTOs.User;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserPresenceService _userPresenceService;
        public UserController(IUserService userService, IUserPresenceService userPresenceService)
        {
            _userService = userService;
            _userPresenceService = userPresenceService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetCurrentUserAsync()
        {
            var currentUser = await _userService.GetCurrentUserAsync();
            return Ok(new ApiResponse<UserDto>
            {
                IsSuccess = true,
                Data = currentUser,
                Message = "Current user details are retrived successfully."
            });
        }

        [HttpGet("userId")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetUserDetailsByUserIdAsync(int userId)
        {
            var user = await _userService.GetUserDetailsByUserIdAsync(userId);
            return Ok(new ApiResponse<UserDto>
            {
                IsSuccess = true,
                Data = user,
                Message = "User details are retrived successfully."
            });
        }

        [HttpGet("{userId}/last-seen")]
        public async Task<ActionResult<DateTime?>> GetLastSeen(string userId)
        {
            var result = await _userPresenceService.GetLastSeen(userId);
            return Ok(result);
        }
    }
}

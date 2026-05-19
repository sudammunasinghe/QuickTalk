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
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse<UserDto>>> GetCurrentUser()
        {
            var currentUser = await _userService.GetCurrentUser();
            return Ok(new ApiResponse<UserDto>
            {
                IsSuccess = true,
                Data = currentUser,
                Message = "Current user details are retrived successfully."
            });
        }
    }
}

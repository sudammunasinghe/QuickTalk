using Microsoft.AspNetCore.Mvc;
using QuickTalk.Application.DTOs.ApiResponse;
using QuickTalk.Application.DTOs.Auth;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<string>>> RegisterUserAsync([FromBody] RegisterDto dto)
        {
            var token = await _authService.RegisterUserAsync(dto);
            return Ok(new ApiResponse<string>
            {
                IsSuccess =  true,
                Data = token,
                Message = "User registration successfull."
            });
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(new ApiResponse<string>
            {
                IsSuccess = true,
                Data = token,
                Message = "Successfully logged."
            });

        }
    }
}

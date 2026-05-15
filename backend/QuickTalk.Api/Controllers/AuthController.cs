using Microsoft.AspNetCore.Mvc;
using QuickTalk.Application.DTOs.ApiResponse;
using QuickTalk.Application.DTOs.User;
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

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> RegisterUserAsync([FromBody] RegisterDto dto)
        {
            await _authService.RegisterUserAsync(dto);
            return Ok(new ApiResponse<string>
            {
                IsSuccess =  true,
                Message = "User registration successfull."
            });
        }
    }
}

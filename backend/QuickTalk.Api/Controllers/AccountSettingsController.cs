using Microsoft.AspNetCore.Mvc;
using QuickTalk.Application.DTOs.AccountSettingsResponse;
using QuickTalk.Application.DTOs.ApiResponse;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountSettingsController : ControllerBase
    {
        private readonly IAccountSettingsService _accountSettingsService;
        public AccountSettingsController(IAccountSettingsService accountSettingsService)
        {
            _accountSettingsService = accountSettingsService;
        }

        [HttpPost("change-password")]
        public async Task<ActionResult<ApiResponse<string>>> ChangePasswordAsync(ChangePasswordDto dto)
        {
            await _accountSettingsService.ChangePasswordAsync(dto);
            return Ok(new ApiResponse<string>
            {
                IsSuccess = true,
                Message = "Password changed successfully..."
            });
        }
    }
}

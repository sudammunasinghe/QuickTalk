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

        [HttpGet("privacy-settings")]
        public async Task<ActionResult<ApiResponse<PrivacySettingsDto>>> GetPrivacySettingsDetailsAsync()
        {
            var result = await _accountSettingsService.GetPrivacySettingsDetailsAsync();
            return Ok(new ApiResponse<PrivacySettingsDto>
            {
                IsSuccess = true,
                Data = result,
                Message = "Privacy setting details retrieved successfully."
            });

        }

        [HttpPut("privacy-settings")]
        public async Task<ActionResult<ApiResponse<PrivacySettingsDto>>> UpdatePrivacySettingsAsync(UpdatePrivacySettings dto)
        {
            var result = await _accountSettingsService.UpdatePrivacySettingsAsync(dto);
            return Ok(new ApiResponse<PrivacySettingsDto>
            {
                IsSuccess = true,
                Data = result,
                Message = "Privacy settings updated successfully."
            });
        }
    }
}

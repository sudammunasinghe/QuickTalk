using Microsoft.AspNetCore.Mvc;
using QuickTalk.Api.Models;
using QuickTalk.Application.DTOs.AccountSettingsResponse;
using QuickTalk.Application.DTOs.ApiResponse;
using QuickTalk.Application.Interfaces.IServices;
using System.Runtime.CompilerServices;

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

        [HttpPut("profile-settings")]
        public async Task<ActionResult<ApiResponse<UpdateProfileResponseDto>>> UpdateProfileDetailsAsync([FromForm] UpdateProfileRequest request)
        {
            var profileDto = new UpdateProfileDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Bio = request.Bio,
                DateOfBirth = request.DateOfBirth,
                RemoveProfileImage = request.RemoveProfileImage,
                profileImage = request.ProfilePicture != null
                    ? new FileDto
                    {
                        FileName = request?.ProfilePicture?.FileName,
                        FileStream = request?.ProfilePicture?.OpenReadStream()
                    } : null
            };
            var result = await _accountSettingsService.UpdateProfileDetailsAsync(profileDto);
            return Ok(new ApiResponse<UpdateProfileResponseDto>
            {
                IsSuccess = true,
                Data = result,
                Message = "Profile updated successfully."
            });
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<UpdateProfileResponseDto>>> GetProfileDetailsAsync()
        {
            var result = await _accountSettingsService.GetProfileDetailsAsync();
            return Ok(new ApiResponse<UpdateProfileResponseDto>
            {
                IsSuccess = true,
                Data = result,
                Message = "Profile details retrieved successfully."
            });
        }
    }
}

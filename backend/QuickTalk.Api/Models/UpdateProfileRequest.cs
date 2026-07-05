using QuickTalk.Application.DTOs.AccountSettingsResponse;

namespace QuickTalk.Api.Models
{
    public class UpdateProfileRequest : ProfileDetailsDto
    {
        public IFormFile? ProfilePicture { get; set; }
    }
}

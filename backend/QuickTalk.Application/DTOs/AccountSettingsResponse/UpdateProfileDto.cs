namespace QuickTalk.Application.DTOs.AccountSettingsResponse
{
    public class UpdateProfileDto : ProfileDetailsDto
    {
        public bool RemoveProfileImage { get; set; }
        public FileDto? profileImage { get; set; }
    }
}

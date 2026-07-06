namespace QuickTalk.Application.DTOs.AccountSettingsResponse
{
    public class UpdateProfileResponseDto : ProfileDetailsDto
    {
        public Guid Id { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}

namespace QuickTalk.Application.DTOs.AccountSettingsResponse
{
    public class UpdatePrivacySettings
    {
        public bool ShowProfilePicture { get; set; }
        public bool ShowOnlineStatus { get; set; }
        public bool ShowLastSeen { get; set; }
        public bool ShowBio { get; set; }
    }
}

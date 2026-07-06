namespace QuickTalk.Domain.Entities
{
    public class PrivacySettings : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool ShowProfilePicture { get; set; }
        public bool ShowOnlineStatus { get; set; }
        public bool ShowLastSeen { get; set; }
        public bool ShowBio { get; set; }

    }
}

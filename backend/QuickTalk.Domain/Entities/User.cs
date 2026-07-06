namespace QuickTalk.Domain.Entities
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; private set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool? IsOnline { get; set; }
        public string? Otp { get; set; }
        public DateTime? OtpExpiry { get; set; }
        public bool? IsUsed { get; set; }
        public DateTime? LastSeen { get; set; }
        private User() { }

        public static User Create(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            DateTime? dateOfBirth
            )
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DateOfBirth = dateOfBirth
            };
            user.ChangePassword(passwordHash);
            return user;
        }

        public void Update(
            string firstName,
            string lastName,
            string bio,
            DateTime? dateOfBirth
            )
        {
            FirstName = firstName;
            LastName = lastName;
            Bio = bio;
            DateOfBirth = dateOfBirth;
        }

        public void ChangePassword(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public static void ValidateFileType(string fileType)
        {

        }
    }
}

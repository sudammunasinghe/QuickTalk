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
        public bool? IsOnline { get; set; }
        public Guid? PasswordResetTokenId { get; set; }
        public string? PasswordResetTokenHash { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
        private User() { }

        public static User Create(
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            DateTime? dateOfBirth
            )
        {
            return new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHash,
                DateOfBirth = dateOfBirth
            };
        }

    }
}

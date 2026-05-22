using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Infrastructure.Security
{
    public class HashingService : IHashingService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string HashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashedPassword);
        }
    }
}

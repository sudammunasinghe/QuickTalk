namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string HashedPassword);
    }
}

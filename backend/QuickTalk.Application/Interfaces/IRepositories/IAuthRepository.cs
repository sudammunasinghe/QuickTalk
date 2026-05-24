using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<int> RegisterUserAsync(User newUser);
    }
}

using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Interfaces.IRepositories
{
    public interface IAccountSettingsRepository
    {
        Task<User?> GetUserByUserIdAsync(int userId);
        Task ChangePasswordAsync(User updatedUser);
    }
}

using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Interfaces.IRepositories
{
    public interface IAccountSettingsRepository
    {
        Task<User?> GetUserByUserIdAsync(int userId);
        Task UpdateUserDetailsAsync(User updatedUser);
        Task<PrivacySettings?> GetPrivacySettingsDetailsAsync(int userId);
        Task UpdatePrivacySettingsAsync(PrivacySettings updatedPrivacySettings);
    }
}

using QuickTalk.Application.DTOs.AccountSettingsResponse;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IAccountSettingsService
    {
        Task ChangePasswordAsync(ChangePasswordDto dto);
    }
}

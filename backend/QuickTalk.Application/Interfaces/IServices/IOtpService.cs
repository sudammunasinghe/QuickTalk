using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IOtpService
    {
        Task<string> GenerateOtpAsync();
        Task SaveOtpAsync(User user, string otp);
    }
}

using QuickTalk.Application.DTOs.Auth;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<string> ForgotPassword(ForgotPasswordDto dto);
        Task ResetPassword(ResetPasswordDto dto);
    }
}

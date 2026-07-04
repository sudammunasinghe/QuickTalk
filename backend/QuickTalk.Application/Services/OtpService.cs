using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Services
{
    public class OtpService : IOtpService
    {
        private readonly IAuthRepository _authRepository;
        public OtpService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<string> GenerateOtpAsync()
        {
            return new Random().Next(100000, 999999).ToString();
        }

        public async Task SaveOtpAsync(User user, string otp)
        {
            user.Otp = otp;
            user.OtpExpiry = DateTime.Now.AddMinutes(3);
            user.LastModifiedDateTime = DateTime.Now;
            user.IsUsed = false;

            await _authRepository.UpdateUserAsync(user);

        }
    }
}

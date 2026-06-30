using QuickTalk.Application.DTOs.AccountSettingsResponse;
using QuickTalk.Application.Exceptions;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Application.Services
{
    public class AccountSettingsService : IAccountSettingsService
    {
        private readonly IAccountSettingsRepository _accountSettingsRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IHashingService _hashingService;
        public AccountSettingsService(
            IAccountSettingsRepository accountSettingsRepository,
            ICurrentUser currentUser,
            IHashingService hashingService
            )
        {
            _accountSettingsRepository = accountSettingsRepository;
            _currentUser = currentUser;
            _hashingService = hashingService;
        }

        public async Task ChangePasswordAsync(ChangePasswordDto dto)
        {
            var loggedUser = _currentUser.UserId;

            if (dto.NewPassword != dto.ConfirmedNewPassword)
                throw new BadRequestException("Passwords do not match.");

            var user =
                await _accountSettingsRepository.GetUserByUserIdAsync(loggedUser);

            if (user == null)
                throw new NotFoundException("User not found.");

            if (!_hashingService.VerifyPassword(dto.CurrentPassword, user.PasswordHash))
                throw new BadRequestException("Current password is incorrect.");

            if (dto.CurrentPassword == dto.NewPassword)
                throw new BadRequestException("You cannot reuse your current password.");

            var newPasswordHash = _hashingService.HashPassword(dto.ConfirmedNewPassword);
            user.ChangePassword(newPasswordHash);
            user.LastModifiedDateTime = DateTime.UtcNow;

            await _accountSettingsRepository.ChangePasswordAsync(user);
        }
    }
}

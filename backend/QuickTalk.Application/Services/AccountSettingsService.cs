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
        private readonly IFileService _fileService;
        public AccountSettingsService(
            IAccountSettingsRepository accountSettingsRepository,
            ICurrentUser currentUser,
            IHashingService hashingService,
            IFileService fileService
            )
        {
            _accountSettingsRepository = accountSettingsRepository;
            _currentUser = currentUser;
            _hashingService = hashingService;
            _fileService = fileService;
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

            await _accountSettingsRepository.UpdateUserDetailsAsync(user);
        }

        public async Task<PrivacySettingsDto> GetPrivacySettingsDetailsAsync()
        {
            var loggedUser = _currentUser.UserId;
            var settings =
                await _accountSettingsRepository.GetPrivacySettingsDetailsAsync(loggedUser);

            if (settings == null)
                throw new NotFoundException("Privacy settings not found.");

            return new PrivacySettingsDto
            {
                Id = settings.Id,
                ShowProfilePicture = settings.ShowProfilePicture,
                ShowOnlineStatus = settings.ShowOnlineStatus,
                ShowLastSeen = settings.ShowLastSeen,
                ShowBio = settings.ShowBio
            };
        }

        public async Task<PrivacySettingsDto> UpdatePrivacySettingsAsync(UpdatePrivacySettings dto)
        {
            var loggedUser = _currentUser.UserId;
            var settings =
                await _accountSettingsRepository.GetPrivacySettingsDetailsAsync(loggedUser);

            if (settings == null)
                throw new NotFoundException("Privacy settings not found.");

            settings.ShowProfilePicture = dto.ShowProfilePicture;
            settings.ShowOnlineStatus = dto.ShowOnlineStatus;
            settings.ShowLastSeen = dto.ShowLastSeen;
            settings.ShowBio = dto.ShowBio;

            await _accountSettingsRepository.UpdatePrivacySettingsAsync(settings);
            return new PrivacySettingsDto
            {
                Id = settings.Id,
                ShowProfilePicture = settings.ShowProfilePicture,
                ShowOnlineStatus = settings.ShowOnlineStatus,
                ShowLastSeen = settings.ShowLastSeen,
                ShowBio = settings.ShowBio
            };
        }

        public async Task<UpdateProfileResponseDto> UpdateProfileDetailsAsync(UpdateProfileDto dto)
        {
            var loggedUser = _currentUser.UserId;

            var user =
                await _accountSettingsRepository.GetUserByUserIdAsync(loggedUser);

            if (user == null)
                throw new NotFoundException("User not found.");

            user.Update(
                dto.FirstName,
                dto.LastName,
                dto.Bio,
                dto.DateOfBirth
            );

            string oldProfileUrl = user.ProfileImageUrl;
            if (dto.profileImage != null)
            {
                var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
                if (!allowedExtensions.Contains(Path.GetExtension(dto.profileImage?.FileName)))
                    throw new BadRequestException("Invalid file type.");

                var subFolder = "profile_images";
                var newProfileUrl =
                    await _fileService.UploadFileAsync(
                        loggedUser,
                        subFolder,
                        dto.profileImage
                    );
                user.ProfileImageUrl = newProfileUrl;

            }
            else if (dto.RemoveProfileImage)
            {
                user.ProfileImageUrl = null;
            }

            if (dto.profileImage != null || dto.RemoveProfileImage)
            {
                if (!string.IsNullOrWhiteSpace(oldProfileUrl))
                {
                    await _fileService.DeleteFileAsync(oldProfileUrl);
                }
            }

            user.LastModifiedDateTime = DateTime.UtcNow;
            await _accountSettingsRepository.UpdateUserDetailsAsync(user);

            return new UpdateProfileResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Bio = user.Bio,
                DateOfBirth = user.DateOfBirth,
                ProfilePictureUrl = user.ProfileImageUrl
            };
        }

        public async Task<UpdateProfileResponseDto> GetProfileDetailsAsync()
        {
            var loggedUser = _currentUser.UserId;
            var user =
                await _accountSettingsRepository.GetUserByUserIdAsync(loggedUser);

            if (user == null)
                throw new NotFoundException("User not found.");

            return new UpdateProfileResponseDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Bio = user.Bio,
                DateOfBirth= user.DateOfBirth,
                ProfilePictureUrl = user.ProfileImageUrl
            };
        }
    }
}

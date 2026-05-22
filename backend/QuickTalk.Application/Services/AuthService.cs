using QuickTalk.Application.DTOs.Auth;
using QuickTalk.Application.Exceptions;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using QuickTalk.Domain.Entities;

namespace QuickTalk.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IHashingService _hashingService;
        private readonly ITokenGenerateService _tokenGenerateService;
        public AuthService(IAuthRepository authRepository, IHashingService hashingService, ITokenGenerateService tokenGenerateService)
        {
            _authRepository = authRepository;
            _hashingService = hashingService;
            _tokenGenerateService = tokenGenerateService;
        }

        public async Task<string> RegisterUserAsync(RegisterDto dto)
        {
            var existingUser =
                await _authRepository.GetUserByEmailAsync(dto.Email);

            if (existingUser != null)
                throw new ConflictException("Email already exists.");

            var passwordHash = _hashingService.HashPassword(dto.Password);
            var newUser = User.Create(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                passwordHash,
                dto.DateOfBirth
            );
            var newUserId = await _authRepository.RegisterUserAsync(newUser);
            newUser.Id = newUserId;
            return _tokenGenerateService.GenerateJwtToken(newUser);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user =
                await _authRepository.GetUserByEmailAsync(dto.Email);

            if (user == null || !_hashingService.VerifyPassword(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");
            return _tokenGenerateService.GenerateJwtToken(user);
        }
    }
}

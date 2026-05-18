using QuickTalk.Application.DTOs.User;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using QuickTalk.Domain.Entities;
using QuickTalk.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

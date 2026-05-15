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
        public AuthService(IAuthRepository authRepository, IHashingService hashingService)
        {
            _authRepository = authRepository;
            _hashingService = hashingService;
        }

        public async Task RegisterUserAsync(RegisterDto dto)
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
            await _authRepository.RegisterUserAsync(newUser);
        }
    }
}

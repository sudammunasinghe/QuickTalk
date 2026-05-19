using QuickTalk.Application.DTOs.User;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using QuickTalk.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IcurrentUser _currentUser;
        public UserService(IUserRepository userRepository, IcurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<UserDto> GetCurrentUser()
        {
            var loggedUser = _currentUser.UserId;

            var user =
                await _userRepository.GetCurrentUser(loggedUser);

            if (user == null)
                throw new NotFoundException("User not found.");

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            };
        }
    }
}

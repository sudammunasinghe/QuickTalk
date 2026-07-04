using QuickTalk.Application.DTOs.User;
using QuickTalk.Application.Exceptions;
using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        public UserService(IUserRepository userRepository, ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<UserDto> GetCurrentUserAsync()
        {
            var loggedUser = _currentUser.UserId;

            var user =
                await _userRepository.GetUserByUserIdAsync(loggedUser);

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

        public async Task<UserDto> GetUserDetailsByUserIdAsync(int userId)
        {
            var user =
                await _userRepository.GetUserByUserIdAsync(userId);

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

        public async Task<IEnumerable<UserDto>> GetPeopleToChat()
        {
            var loggedUser = _currentUser.UserId;
            var userDetails =
                await _userRepository.GetPeopleToChat(loggedUser);

            return userDetails.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth
            });
        }
    }
}

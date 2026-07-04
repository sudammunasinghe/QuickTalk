using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using QuickTalk.Application.Models;

namespace QuickTalk.Application.Services
{
    public class UserPresenceService : IUserPresenceService
    {
        private static readonly Dictionary<string, UserPresence> _users = new();
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;

        public UserPresenceService(IAuthRepository authRepository, IUserRepository userRepository)
        {
            _authRepository = authRepository;
            _userRepository = userRepository;
        }

        public Task UserOnline(string userId, string connectionId)
        {
            if (!_users.ContainsKey(userId))
                _users[userId] = new UserPresence();

            var user = _users[userId];
            user.Connections.Add(connectionId);
            user.LastActivity = DateTime.Now;
            user.LastSeen = null;

            return Task.CompletedTask;
        }

        public async Task UserOffline(string userId, string connectionId)
        {
            if (!_users.ContainsKey(userId))
                return;

            var user = _users[userId];
            user.Connections.Remove(connectionId);

            //user closed all devices
            if (user.Connections.Count == 0)
            {
                if (!int.TryParse(userId, out var id))
                    return;

                //Get user details
                var currentUserDetails =
                    await _userRepository.GetUserByUserIdAsync(id);

                //save to DB
                currentUserDetails.LastSeen = DateTime.Now;
                await _authRepository.UpdateUserAsync(currentUserDetails);
                _users.Remove(userId);
            }
        }

        public Task UpdateActivity(string userId)
        {
            if (_users.ContainsKey(userId))
            {
                _users[userId].LastActivity = DateTime.Now;
            }
            return Task.CompletedTask;
        }

        public string GetStatus(string userId)
        {
            if (!_users.ContainsKey(userId))
            {
                return "Offline";
            }
            var user = _users[userId];
            var inactiveTime = DateTime.Now - user.LastActivity;

            if (inactiveTime.TotalMinutes >= 5)
                return "Away";
            return "Online";
        }

        public async Task<DateTime?> GetLastSeen(string userId)
        {
            if (!int.TryParse(userId, out var id))
                return null;

            var user =
                await _userRepository.GetUserByUserIdAsync(id);

            if (user == null)
                return null;

            return user.LastSeen;
        }

        public List<string> GetOnlineUsers()
        {
            return _users.Keys.ToList();
        }

        public List<string> GetUsersWhoAreAway()
        {
            var now = DateTime.Now;
            var awayThreshold = TimeSpan.FromMinutes(5);

            return _users
                .Where(x =>
                    x.Value.Connections.Count > 0 &&
                    (now - x.Value.LastActivity) > awayThreshold)
                .Select(x => x.Key)
                .ToList();
        }
    }
}

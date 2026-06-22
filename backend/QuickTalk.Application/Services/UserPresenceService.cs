using QuickTalk.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Services
{
    public class UserPresenceService: IUserPresenceService
    {
        private static readonly Dictionary<string, HashSet<string>> _onlineUsers = new();

        public Task UserOnline(string userId, string connectionId)
        {
            if(!_onlineUsers.ContainsKey(userId))
                _onlineUsers[userId] = new HashSet<string>();

            _onlineUsers[userId].Add(connectionId);
            return Task.CompletedTask;
        }

        public Task UserOffline(string userId, string connectionId)
        {
            if (_onlineUsers.ContainsKey(userId))
            {
                _onlineUsers[userId].Remove(connectionId);

                if (_onlineUsers[userId].Count == 0)
                    _onlineUsers.Remove(userId);
            }
            return Task.CompletedTask;
        }

        public bool IsOnline(string userId)
        {
            return _onlineUsers.ContainsKey(userId);
        }

        public List<string> GetOnlineUsers()
        {
            return _onlineUsers.Keys.ToList();
        }
    }
}

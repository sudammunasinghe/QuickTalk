using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Interfaces.IServices
{
    public interface IUserPresenceService
    {
        Task UserOnline(string userId, string connectionId);
        Task UserOffline(string userId, string connectionId);
        Task UpdateActivity(string userId);
        string GetStatus(string userId);
        Task<DateTime?> GetLastSeen(string userId);
        List<string> GetOnlineUsers();
        List<string> GetUsersWhoAreAway();

    }
}

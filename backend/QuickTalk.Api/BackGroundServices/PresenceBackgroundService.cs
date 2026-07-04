using Microsoft.AspNetCore.SignalR;
using QuickTalk.Api.Hub;
using QuickTalk.Application.Interfaces.IServices;

namespace QuickTalk.Api.BackGroundServices
{
    public class PresenceBackgroundService : BackgroundService
    {
        private readonly IUserPresenceService _userPresenceService;
        private readonly IHubContext<ChatHub> _hub;

        public PresenceBackgroundService(IUserPresenceService userPresenceService, IHubContext<ChatHub> hub)
        {
            _userPresenceService = userPresenceService;
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var awayUsers = _userPresenceService.GetUsersWhoAreAway();
                foreach (var userId in awayUsers)
                {
                    await _hub.Clients.All
                        .SendAsync(
                            "UserStatusChanged",
                            userId,
                            "Away"
                        );
                }

                await Task.Delay(
                    TimeSpan.FromMinutes(1),
                    stoppingToken);
            }
        }
    }
}

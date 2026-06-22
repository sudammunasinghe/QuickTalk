namespace QuickTalk.Api.Hub;
using Microsoft.AspNetCore.SignalR;
using QuickTalk.Application.Interfaces.IServices;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    private readonly IUserPresenceService _userPresenceService;
    private readonly ICurrentUser _currentUser;

    public ChatHub(IUserPresenceService userPresenceService, ICurrentUser currentUser)
    {
        _currentUser = currentUser;
        _userPresenceService = userPresenceService;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = _currentUser.UserId.ToString();
        var connectionId = Context.ConnectionId;

        await _userPresenceService.UserOnline(userId, connectionId);

        //notify others
        await Clients.AllExcept(connectionId)
            .SendAsync("UserStatusChanged", userId, "Online");

        //send current online users to newly connected user
        var onlineUsers = _userPresenceService.GetOnlineUsers();

        await Clients.Caller.SendAsync("OnlineUsers", onlineUsers);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = _currentUser?.UserId.ToString();
        var connectionId = Context.ConnectionId;

        await _userPresenceService.UserOffline(userId, connectionId);

        await Clients.All
            .SendAsync("UserStatusChanged", userId, "Offline");

        await base.OnDisconnectedAsync(exception);
    }
}


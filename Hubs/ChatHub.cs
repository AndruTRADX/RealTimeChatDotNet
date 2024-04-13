using Microsoft.AspNetCore.SignalR;
using RealTimeChat.DataService;
using RealTimeChat.Models;

namespace RealTimeChat.Hubs;

public class ChatHub(SharedDb sharedDb) : Hub
{
    private readonly SharedDb _shared = sharedDb;

    public async Task JoinChat(UserConnection connection)
    {
        await Clients.Others.SendAsync("ReceiveMessage", "admin", $"{connection.Username} has joined.");
    }

    public async Task JoinSpecificChatRoom(UserConnection connection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

        _shared.Connections[Context.ConnectionId] = connection;

        await Clients.Group(connection.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{connection.Username} has joined {connection.ChatRoom}.");
    }

    public async Task SendMessage(string msg)
    {
        if (_shared.Connections.TryGetValue(Context.ConnectionId, out UserConnection connection))
        {
            await Clients.Group(connection.ChatRoom).SendAsync("ReceiveMessage", connection.Username, msg);
        }
    }
}
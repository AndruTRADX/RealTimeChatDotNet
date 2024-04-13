using System.Collections.Concurrent;
using RealTimeChat.Models;

namespace RealTimeChat.DataService;

public class SharedDb
{
    private readonly ConcurrentDictionary<string, UserConnection> _connections = new();
    public ConcurrentDictionary<string, UserConnection> Connections => _connections;
}
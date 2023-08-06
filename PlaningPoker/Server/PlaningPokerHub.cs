using Microsoft.AspNetCore.SignalR;
using PlaningPoker.Shared;

namespace PlaningPoker.Server;

public sealed class PlaningPokerHub : Hub<IPlaningPokerHub>
{
    private readonly PlayerStore _store;

    public PlaningPokerHub(PlayerStore store)
    {
        _store = store;
    }

    public override Task OnConnectedAsync()
    {
        _store.Players.TryAdd(Context.ConnectionId, Player.Empty);

        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _store.Players.Remove(Context.ConnectionId);

        await Clients.All.PlayerDisconnected(_store.Players.Values.ToArray());

        await base.OnDisconnectedAsync(exception);
    }

    public async Task PlayerConnected(Guid id, string username)
    {
        Player player = new(id, username);
        if (_store.Players.TryGetValue(Context.ConnectionId, out _))
        {
            _store.Players[Context.ConnectionId] = player;
        }
        else
        {
            _store.Players.TryAdd(Context.ConnectionId, player);
        }

        await Clients.All.ReceivePlayerConnected(_store.Players.Values.ToArray());
    }

    public async Task Vote(int vote)
    {
        var player = _store.Players[Context.ConnectionId] with
        {
            Vote = vote
        };

        _store.Players[Context.ConnectionId] = player;

        await Clients.All.PlayerVoted(player);
    }

    public async Task NewVote()
    {
        foreach (var key in _store.Players.Keys)
        {
            _store.Players[key] = _store.Players[key] with
            {
                Vote = null
            };
        }

        await Clients.All.NewVote(_store.Players.Values.ToArray());
    }
}

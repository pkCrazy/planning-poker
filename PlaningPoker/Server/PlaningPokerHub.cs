using Microsoft.AspNetCore.SignalR;
using PlaningPoker.Shared;

namespace PlaningPoker.Server;

public sealed class PlaningPokerHub : Hub<IPlaningPokerHub>
{
    private readonly PlayerStore _players;

    public PlaningPokerHub(PlayerStore players)
    {
        _players = players;
    }

    public override Task OnConnectedAsync()
    {
        _players.TryAddPlayer(Context.ConnectionId, Player.Empty);

        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _players.Remove(Context.ConnectionId);

        await Clients.All.PlayerDisconnected(_players.ToArray());

        await base.OnDisconnectedAsync(exception);
    }

    public async Task PlayerConnected(Guid id, string username)
    {
        _players.AddPlayer(Context.ConnectionId, new Player(id, username));

        await Clients.All.ReceivePlayerConnected(_players.ToArray());
    }

    public async Task Vote(int vote)
    {
        _players.SetPlayerVote(Context.ConnectionId, vote);

        await Clients.All.PlayerVoted(_players[Context.ConnectionId]);
    }

    public async Task NewVote()
    {
        _players.ResetVotes();

        await Clients.All.NewVote(_players.ToArray());
    }
}

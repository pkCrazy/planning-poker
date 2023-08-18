using Microsoft.AspNetCore.SignalR;
using PlaningPoker.Shared;

namespace PlaningPoker.Server;

public sealed class PlaningPokerHub : Hub<IPlaningPokerHub>
{
    private readonly RoomState _state;

    public PlaningPokerHub(RoomState state)
    {
        _state = state;
    }

    public override Task OnConnectedAsync()
    {
        _state.Players.TryAddPlayer(Context.ConnectionId, Player.Empty);

        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _state.Players.Remove(Context.ConnectionId);

        await Clients.All.PlayerDisconnected(_state.Players.ToArray());

        await base.OnDisconnectedAsync(exception);
    }

    public async Task PlayerConnected(string username)
    {
        _state.Players.AddPlayer(Context.ConnectionId, new Player(Guid.NewGuid(), username));

        await Clients.All.PlayerConnected(_state.Players.ToArray(), _state.AreVotesVisible);
    }

    public async Task Vote(int vote)
    {
        _state.Players.SetPlayerVote(Context.ConnectionId, vote);

        await Clients.All.PlayerVoted(_state.Players[Context.ConnectionId]);
    }

    public async Task NewVote()
    {
        _state.AreVotesVisible = false;
        _state.Players.ResetVotes();

        await Clients.All.NewVote(_state.Players.ToArray());
    }

    public async Task ShowVotes()
    {
        _state.AreVotesVisible = true;
        await Clients.All.ShowVotes();
    }
}

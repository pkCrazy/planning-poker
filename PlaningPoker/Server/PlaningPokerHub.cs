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
		_store.Players.TryAdd(Context.ConnectionId, Player.Empty());

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

	public async Task Vote(Guid id, int vote)
	{
	}
}

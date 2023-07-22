using Microsoft.AspNetCore.SignalR;

namespace PlaningPoker.Server;

public sealed class PlaningPokerHub : Hub<IPlaningPokerHub>
{
	public async Task Vote(string user, uint? vote)
	{
		await Clients.All.ReceiveVote(user, vote);
	}
}

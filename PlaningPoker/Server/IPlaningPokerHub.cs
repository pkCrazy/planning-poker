using PlaningPoker.Shared;

namespace PlaningPoker.Server;

public interface IPlaningPokerHub
{
    Task ReceivePlayerConnected(IEnumerable<Player> players);

    Task PlayerDisconnected(IEnumerable<Player> players);

    Task ReceiveVote(Guid id, int vote);
}

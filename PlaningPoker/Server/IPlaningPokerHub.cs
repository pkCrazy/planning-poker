using PlaningPoker.Shared;

namespace PlaningPoker.Server;

public interface IPlaningPokerHub
{
    Task PlayerConnected(IEnumerable<Player> players, bool areVotesVisible);

    Task PlayerDisconnected(IEnumerable<Player> players);

    Task PlayerVoted(Player player);

    Task NewVote(IEnumerable<Player> players);

    Task ShowVotes();
}

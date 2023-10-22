namespace PlaningPoker.Shared;

public interface IPlanningPokerServer
{
    Task PlayerConnected(string username);

    Task Vote(int vote);

    Task NewVote();

    Task ShowVotes();
}

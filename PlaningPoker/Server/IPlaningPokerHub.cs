namespace PlaningPoker.Server;

public interface IPlaningPokerHub
{
    Task ReceiveVote(string user, uint? vote);
}

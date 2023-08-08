namespace PlaningPoker.Server;

public sealed class RoomState
{
    public bool AreVotesVisible { get; set; } = false;

    public PlayerStore Players { get; } = new();
}

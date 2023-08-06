using PlaningPoker.Shared;

namespace PlaningPoker.Server;

public sealed class PlayerStore
{
    public Dictionary<string, Player> Players { get; } = new(10);
}

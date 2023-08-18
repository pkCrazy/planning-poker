namespace PlaningPoker.Shared;

public record Player(Guid Id, string Name)
{
    private static Player? _instance;

    public static Player Empty => _instance ??= new(Guid.Empty, string.Empty);

    public int? Vote { get; init; }
}

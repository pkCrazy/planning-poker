namespace PlaningPoker.Shared;

public record Player(Guid Id, string Username)
{
    public static Player Empty() => new(Guid.Empty, string.Empty);
}

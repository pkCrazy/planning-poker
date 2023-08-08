using PlaningPoker.Shared;

namespace PlaningPoker.Server;

public sealed class PlayerStore
{
    private const int ExpectedMaximumPlayers = 10;

    private readonly Dictionary<string, Player> _players = new(ExpectedMaximumPlayers);

    public Player this[string key] => _players[key];

    public bool TryAddPlayer(string key, Player player)
    {
        return _players.TryAdd(key, player);
    }

    public void AddPlayer(string key, Player player)
    {
        if (_players.ContainsKey(key))
        {
            _players[key] = player;
        }
        else
        {
            _players.TryAdd(key, player);
        }
    }

    public bool Remove(string key)
    {
        return _players.Remove(key);
    }

    public void SetPlayerVote(string key, int vote)
    {
        var player = _players[key] with
        {
            Vote = vote
        };

        _players[key] = player;
    }

    public void ResetVotes()
    {
        foreach (var key in _players.Keys)
        {
            _players[key] = _players[key] with
            {
                Vote = null
            };
        }
    }

    public Player[] ToArray()
    {
        return _players.Values.ToArray();
    }
}

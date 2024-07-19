using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager
{
    public PlayerManager(List<string> roles)
    {
        foreach (string role in roles)
        {
            players.Add(new(role));
        }
    }
    public Player GetActivePlayer()
    {
        players.Insert(0, players[^1]);
        players.RemoveAt(players.Count - 1);
        return players[0];
    }
    private readonly List<Player> players = new();
}

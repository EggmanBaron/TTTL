using System.Collections.Generic;

public class PlayerManager
{
    public PlayerManager(List<string> roles)
    {
        foreach (string role in roles)
        {
            m_players.Add(new(role));
        }
    }
    public Player ActivePlayer { get { return m_players[0]; } }
    public void ActivateNextPlayer()
    {
        m_players.Insert(0, m_players[^1]);
        m_players.RemoveAt(m_players.Count - 1);
    }
    private readonly List<Player> m_players = new();
}

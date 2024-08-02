public class Player
{
    public Player(string role)
    {
        Role = role;
    }
    public string Role { get; private set; }
    public bool IsWinner { get; set; }
}

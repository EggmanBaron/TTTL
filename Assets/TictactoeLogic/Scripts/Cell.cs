using UnityEngine;

namespace Assets.TictactoeLogic.Scripts
{
    public struct Cell
    {
        public Cell(int x, int y, string role)
        {
            Role = role;
            Coordinate = new Vector2Int(x, y);
        }
        public Cell(Vector2Int coordinate)
        {
            Role = null;
            Coordinate = coordinate;
        }
        public Cell(Vector2Int coordinate, string role)
        {
            Role = role;
            Coordinate = coordinate;
        }
        public Vector2Int Coordinate { get; set; }
        public string Role { get; set; }
    }
}

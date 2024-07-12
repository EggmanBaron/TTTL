using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace Assets.TictactoeLogic.Scripts
{
    public class Cell
    {
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
        public string Role
        {
            get { return _role; }
            set { _role ??= value; }
        }
        private string _role;
        public int X { get; private set; }
        public int Y { get; private set; }
    }
}

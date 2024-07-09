using Unity.VisualScripting.YamlDotNet.Core.Tokens;

namespace Assets.TictactoeLogic.Scripts
{
    public class Cell
    {
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public string Role
        {
            get { return _role; }
            set { _role ??= value; }
        }
        private string _role;
        public readonly int x;
        public readonly int y;
    }
}

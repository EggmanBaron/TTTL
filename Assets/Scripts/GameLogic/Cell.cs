using System.Collections.Generic;

namespace Assets.Scripts.GameLogic
{
    public class Cell
    {
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Role Role
        {
            get { return role; }
            set
            {
                if (role == Role.None)
                {
                    role = value;
                }
            }
        }
        private Role role = Role.None;
        public readonly int x;
        public readonly int y;
    }
}

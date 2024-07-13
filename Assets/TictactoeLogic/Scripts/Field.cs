using System.Collections.Generic;
using System.Linq;
namespace Assets.TictactoeLogic.Scripts
{
    public class Field
    {
        public Field(GameSettings gameSettings)
        {
            this.gameSettings = gameSettings;
            var size = this.gameSettings.startFieldSize;
            Cells = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cell cell = new(i, j);
                    Cells[i, j] = cell;
                }
            }
        }
        public void EnlargeField()
        {
            var increment = gameSettings.enlargeFieldStep;
            var fieldSize = Cells.GetLength(0);
            var fieldSizeNew = increment * 2 + fieldSize;
            var enlargedField = new Cell[fieldSizeNew, fieldSizeNew];
            for (int i = 0; i < fieldSizeNew; i++)
            {
                for (int j = 0; j < fieldSizeNew; j++)
                {
                    List<bool> conditions = new()
                    {
                        i - increment >= 0,
                        j - increment >= 0,
                        i - increment < fieldSize,
                        j - increment < fieldSize
                    };
                    if (conditions.Contains(false))
                    {
                        enlargedField[i, j] = new Cell(i, j);
                    }
                    else
                    {
                        Cell cell = new(i, j)
                        {
                            Role = Cells[i - increment, j - increment].Role
                        };
                        enlargedField[i, j] = cell;
                    }
                }
            }
            Cells = enlargedField;
        }
        public bool WinCheck(int i, int j)
        {
            int winSize = gameSettings.winlineSize;
            int fieldMax = Cells.GetLength(0) - 1;
            int min = (i - winSize < 0) ? 0 : i - winSize;
            int max = (j + winSize > fieldMax) ? fieldMax : j + winSize;
            string role = Cells[i, j].Role;
            List<bool> conditions = new()
            {
                LineCheck(role, LineType.Horizontal, min, max, jCurrent: j),
                LineCheck(role, LineType.Vertical, min, max, iCurrent: i),
                LineCheck(role, LineType.Diagonal, min, max),
                LineCheck(role, LineType.Antidiagonal, min, max)
            };
            return conditions.Contains(true);
        }
        private bool LineCheck(string role, LineType lineType, int min, int max, int iCurrent = -1, int jCurrent = -1)
        {
            bool result = false;
            int winSize = gameSettings.winlineSize;
            List<Cell> siblings = new();
            for (int x = min; x <= max; x++)
            {
                int i;
                int j;
                if (lineType == LineType.Horizontal) { i = x; j = jCurrent; }
                else if (lineType == LineType.Vertical) { i = iCurrent; j = x; }
                else if (lineType == LineType.Diagonal) { i = x; j = x; }
                else { i = x; j = max - x; }
                i = (i < 0) ? 0 : i;
                j = (j < 0) ? 0 : j;
                Cell cell = Cells[i, j];
                if (cell.Role == role)
                {
                    siblings.Add(cell);
                    if (siblings.Count >= winSize) { result = true; break; }
                }
                else { siblings = new(); }
            }
            return result;
        }
        public bool MakeMove(int i, int j, string role)
        {
            Cell cell = Cells[i, j];
            if (cell.Role == null)
            {
                cell.Role = role;
                LastChanged = cell;
                return true;
            }
            else return false;
        }
        public override string ToString()
        {
            string beginning = "`   ,";
            string empty = "    ,";
            string marked = " {0} ,";
            string indices = "{0}: ,";
            string newString = "\n";
            string result = beginning;
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                result += empty;
            }
            result += newString;
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                result += string.Format(indices, i.ToString());
                for (int j = 0; j < Cells.GetLength(0); j++)
                {
                    string role = Cells[i, j].Role;
                    if (role == null) { result += "    ,"; }
                    else { result += string.Format(marked, role[0].ToString()); }
                }
                result += newString;
            }
            return result;
        }
        public Cell LastChanged { get; private set; }
        public Cell[,] Cells { get; private set; }
        public int Size { get { return Cells.GetLength(0); } }
        private readonly GameSettings gameSettings;
        private enum LineType
        {
            Horizontal,
            Vertical,
            Diagonal,
            Antidiagonal
        }
    }
}

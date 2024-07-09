using System.Collections.Generic;
using System.Linq;
namespace Assets.TictactoeLogic.Scripts
{
    public class Field
    {
        public Field(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            var size = _gameSettings.startFieldSize;
            _cells = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cell cell = new(i, j);
                    _cells[i, j] = cell;
                }
            }
        }
        public void EnlargeField()
        {
            var increment = _gameSettings.enlargeFieldStep;
            var fieldSize = _cells.GetLength(0);
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
                            Role = _cells[i - increment, j - increment].Role
                        };
                        enlargedField[i, j] = cell;
                    }
                }
            }
            _cells = enlargedField;
        }
        public bool WinCheck(int i, int j)
        {
            int winSize = _gameSettings.winlineSize;
            int fieldMax = _cells.GetLength(0) - 1;
            int min = (i - winSize < 0) ? 0 : i - winSize;
            int max = (j + winSize > fieldMax) ? fieldMax : j + winSize;
            string role = _cells[i, j].Role;
            // Role role = _cells[i, j].Role;
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
            int winSize = _gameSettings.winlineSize;
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
                Cell cell = _cells[i, j];
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
            Cell cell = _cells[i, j];
            if (cell.Role == null)
            {
                cell.Role = role;
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
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                result += empty;
            }
            result += newString;
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                result += string.Format(indices, i.ToString());
                for (int j = 0; j < _cells.GetLength(0); j++)
                {
                    string role = _cells[i, j].Role;
                    if (role == null) { result += "    ,"; }
                    else { result += string.Format(marked, role[0].ToString()); }
                }
                result += newString;
            }
            return result;
        }
        public Cell[,] Cells
        {
            get { return _cells; }
        }
        public Cell[,] WinLine
        {
            get { return _winLine; }
        }
        public int Size
        {
            get { return _cells.GetLength(0); }
        }
        private Cell[,] _cells;
        private readonly GameSettings _gameSettings;
        private Cell[,] _winLine;
        private enum LineType
        {
            Horizontal,
            Vertical,
            Diagonal,
            Antidiagonal
        }
    }
}

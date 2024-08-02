using System.Collections.Generic;
using System.Linq;
using GluonGui.Dialog;
namespace Assets.TictactoeLogic.Scripts
{
    public class Field
    {
        public Field(int size, int winLineSize)
        {
            m_winLineSize = winLineSize;
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
        public void EnlargeField(int increment)
        {
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
            int fieldMax = Cells.GetLength(0) - 1;
            int min = (m_i - m_winLineSize < 0) ? 0 : m_i - m_winLineSize;
            int max = (m_j + m_winLineSize > fieldMax) ? fieldMax : m_j + m_winLineSize;
            string role = Cells[i, j].Role;
            List<bool> conditions = new()
            {
                LineCheck(LineType.Horizontal, min, max),
                LineCheck(LineType.Vertical, min, max),
                LineCheck(LineType.Diagonal, min, max),
                LineCheck(LineType.Antidiagonal, min, max)
            };
            return conditions.Contains(true);
        }
        private bool LineCheck(LineType lineType, int min, int max)
        {
            bool result = false;
            List<Cell> siblings = new();
            for (int x = min; x <= max; x++)
            {
                int i;
                int j;
                if (lineType == LineType.Horizontal) { i = x; j = m_j; }
                else if (lineType == LineType.Vertical) { i = m_i; j = x; }
                else if (lineType == LineType.Diagonal) { i = x; j = x; }
                else { i = x; j = max - x; }
                i = (i < 0) ? 0 : i;
                j = (j < 0) ? 0 : j;
                Cell cell = Cells[i, j];
                if (cell.Role == m_role)
                {
                    siblings.Add(cell);
                    if (siblings.Count >= m_winLineSize) { result = true; break; }
                }
                else { siblings = new(); }
            }
            return result;
        }
        public bool MakeMove(int i, int j, string role)
        {
            m_i = i;
            m_j = j;
            m_role = role;
            Cell cell = Cells[m_i, m_j];
            if (cell.Role == null)
            {
                cell.Role = m_role;
                return true;
            }
            else return false;
        }
        public bool IsFull()
        {
            bool result = true;
            foreach (Cell cell in Cells)
            {
                if (cell.Role == null) result = false;
                break;
            }
            return result;
        }
        public override string ToString()
        {
            string beginning = "+ .";
            string empty = "    .";
            string indicesY = " {0} .";
            string indicesX = "{0}: .";
            string marked = " {0} .";
            string newString = "\n";
            string result = beginning;
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                result += string.Format(indicesY, i.ToString());
            }
            result += newString;
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                result += string.Format(indicesX, i.ToString());
                for (int j = 0; j < Cells.GetLength(0); j++)
                {
                    string role = Cells[i, j].Role;
                    if (role == null) { result += empty; }
                    else { result += string.Format(marked, role[0].ToString()); }
                }
                result += newString;
            }
            return result;
        }
        public Cell LastChanged { get { return Cells[m_i, m_j]; } }
        public Cell[,] Cells { get; private set; }
        public int Dimention { get { return Cells.GetLength(0); } }
        public string CurrentRole { get { return m_role; } }
        private readonly int m_winLineSize;
        private int m_i;
        private int m_j;
        private string m_role;
        private enum LineType
        {
            Horizontal,
            Vertical,
            Diagonal,
            Antidiagonal
        }
    }
}

using System.Collections.Generic;
namespace Assets.TictactoeLogic.Scripts
{
    public class FieldModel
    {
        public FieldModel(int size, int winLineSize)
        {
            m_winLineSize = winLineSize;
            Cells = new Cell[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    // Cell cell = new(i, j);
                    // Cells[i, j] = cell;
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
                        // enlargedField[i, j] = new Cell(i, j);
                    }
                    else
                    {
                        // Cell cell = new(i, j)
                        // {
                        //     Role = Cells[i - increment, j - increment].Role
                        // };
                        // enlargedField[i, j] = cell;
                    }
                }
            }
            Cells = enlargedField;
        }
        private void AddRowTop()
        {
            AddRow(0, 1, Height + 1);
        }

        private void AddRowBottom()
        {
            AddRow(Height, 0, Height - 1);
        }
        private void AddRow(int iAdd, int iShiftMin, int iShiftMax)
        {
            int newHeight = Height + 1;
            var enlargedField = new Cell[Width, newHeight];
            // for (int j = 0; j < Width; j++)
            // {
            //     enlargedField[iAdd, j] = new Cell(iAdd, j);
            // }
            // for (int i = iShiftMin; i < iShiftMax; i++)
            //     for (int j = 0; j < Width; j++)
            //     {
            //         Cell cell = new(i, j)
            //         {
            //             Role = Cells[i - 1, j].Role
            //         };
            //         enlargedField[i, j] = cell;
            //     }
        }
        private void AddLine
        (
            int horizontalIndex = 0,
            int verticalIndex = 0,
            int iShiftMin = 0,
            int iShiftMax = 0,
            int jShiftMin = 0,
            int jShiftMax = 0
        )
        {
            int newHeight = (horizontalIndex == 0) ? Height : Height + 1;
            int newWidth = (verticalIndex == 0) ? Width : Width + 1;
            var enlargedField = new Cell[newWidth, newHeight];
            // int iMin = 0;
            // int iMax = 0;
            // int jMim = 0;
            // int jMax = 0;
            // for (int i = iMin; i < iMax; i++)
            // {
            //     for (int j = jMim; j < jMax; j++)
            //     {
            //         enlargedField[i, j] = new(i, j);
            //     }
            // }
        }
        private void AddColumn()
        {
            int newWidth = Width + 1;
            var enlargedField = new Cell[newWidth, Height];
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
            bool result = cell.Role == null;
            cell.Role ??= m_role;
            return result;
        }
        public bool IsFull()
        {
            bool result = true;
            foreach (Cell cell in Cells)
            {
                result = cell.Role != null;
                if (!result) break;
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
            for (int i = 0; i < Height; i++)
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
        private int Width { get { return Cells.GetLength(0); } }
        private int Height { get { return Cells.GetLength(1); } }
        private enum LineType
        {
            Horizontal,
            Vertical,
            Diagonal,
            Antidiagonal
        }
    }
}

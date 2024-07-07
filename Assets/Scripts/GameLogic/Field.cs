using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using GluonGui.Dialog;
using PlasticGui;
using UnityEngine;
using UnityEngine.WSA;
namespace Assets.Scripts.GameLogic
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
            var newFieldSize = increment * 2 + _cells.GetLength(0);
            var enlargedField = new Cell[newFieldSize, newFieldSize];
            for (int i = 0; i < newFieldSize; i++)
            {
                for (int j = 0; j < newFieldSize; j++)
                {
                    int iShifted = i - increment;
                    int jShifted = j - increment;
                    bool iCondition = 0 <= iShifted && iShifted < _cells.GetLength(0);
                    bool jCondition = 0 <= jShifted && jShifted < _cells.GetLength(0);
                    if (iCondition && jCondition)
                    {
                        Cell cell = new(i, j);
                        cell.Role = _cells[iShifted, jShifted].Role;
                        enlargedField[i, j] = cell;
                    }
                    else
                    {
                        enlargedField[i, j] = new Cell(i, j);
                    }
                }
            }
            _cells = enlargedField;
        }
        public bool WinCheck(int i, int j)
        {
            bool result = false;
            int winSize = _gameSettings.winlineSize;
            int fieldMax = _cells.GetLength(0) - 1;
            int min = (i - winSize < 0) ? 0 : i - winSize;
            int max = (j + winSize > fieldMax) ? fieldMax : j + winSize;
            Role role = _cells[i, j].Role;
            bool hor = LineCheck(role, LineType.Horizontal, min, max, jCurrent: j);
            bool vert = LineCheck(role, LineType.Vertical, min, max, iCurrent: i);
            bool diag = LineCheck(role, LineType.Diagonal, min, max);
            bool antiDiag = LineCheck(role, LineType.Antidiagonal, min, max);
            if (hor || vert || diag || antiDiag) { result = true; }
            return result;
        }
        private bool LineCheck(Role role, LineType lineType, int min, int max, int iCurrent = -1, int jCurrent = -1)
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
        public bool SetCross(int i, int j)
        {
            Cell cell = _cells[i, j];
            if (cell.Role == Role.None)
            {
                cell.Role = Role.Cross;
                return true;
            }
            return false;
        }
        public bool SetZero(int i, int j)
        {
            if (_cells[i, j].Role == Role.None)
            {
                _cells[i, j].Role = Role.Zero;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            string result = string.Empty;
            for (int i = 0; i < _cells.GetLength(0); i++)
            {
                result += string.Format("{0}: ", i.ToString());
                for (int j = 0; j < _cells.GetLength(0); j++)
                {
                    switch (_cells[i, j].Role)
                    {
                        case Role.Zero:
                            result += "[ o ]";
                            continue;
                        case Role.Cross:
                            result += "[ x ]";
                            continue;
                        case Role.None:
                            result += "[   ]";
                            continue;
                    }
                }
                result += "\n";
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
        public Role Winner
        {
            get { return _winner; }
        }
        public int Size
        {
            get { return _cells.GetLength(0); }
        }
        private Cell[,] _cells;
        private readonly GameSettings _gameSettings;
        private Role _winner;
        private Cell[,] _winLine;
    }
}

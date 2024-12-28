using System;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    public Field(int width, int height)
    {
        StartPoint = new(0, 0);
        Size = new(width, height);

    }
    public void Print()
    {
        for (int j = Size.y - StartPoint.y - 1; j >= StartPoint.y; j--)
        {
            string row = string.Empty;
            for (int i = StartPoint.x; i < Size.x - StartPoint.x; i++)
            {
                var role = GetCell(i, j).Role ?? "null";
                row += string.Format("{0}:\t({1}, {2})\t|", role, i, j);
            }
            Debug.Log(row);
        }
    }
    public void AddColumnsLeft(int number)
    {
        Vector2Int index_shift = new(-number, 0);
        Size += new Vector2Int(Math.Abs(index_shift.x), Math.Abs(index_shift.y));
        StartPoint += index_shift;
    }
    public void AddColumnsRight(int number)
    {
        Vector2Int index_shift = new(number, 0);
        Size += new Vector2Int(Math.Abs(index_shift.x), Math.Abs(index_shift.y));
    }
    public void AddRowsUp(int number)
    {
        Vector2Int index_shift = new(0, number);
        Size += new Vector2Int(Math.Abs(index_shift.x), Math.Abs(index_shift.y));
    }
    public void AddRowsDown(int number)
    {
        Vector2Int index_shift = new(0, -number);
        Size += new Vector2Int(Math.Abs(index_shift.x), Math.Abs(index_shift.y));
        StartPoint += index_shift;
    }
    public Cell GetCell(Vector2Int index)
    {
        return m_cells.ContainsKey(index) ? m_cells[index] : new Cell(index);
    }
    public Cell GetCell(int x, int y)
    {
        Vector2Int index = new(x, y);
        return GetCell(index);
    }
    public void SetCell(Cell cell)
    {
        m_cells[cell.Coordinate] = cell;
    }
    public Vector2Int Size { get; private set; }
    public Vector2Int StartPoint { get; private set; }
    private readonly Dictionary<Vector2Int, Cell> m_cells = new();

    private class Cell
    {
        private Cell(int x, int y, string role)
        {
            Role = role;
            Coordinate = new Vector2Int(x, y);
        }

        public string Role { get; private set; }
        public Vector2Int Coordinate { get; private set; }
    }
}
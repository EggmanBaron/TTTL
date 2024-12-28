using NUnit.Framework;
using Assets.TictactoeLogic.Scripts;
using UnityEngine;

public class FieldTests
{
    [Test]
    public void FieldTests_GetSetCell()
    {
        m_field.SetCell(new Cell(0, 0, c_cross));
        m_field.SetCell(new Cell(1, 1, c_cross));
        m_field.SetCell(new Cell(2, 2, c_cross));
        m_field.SetCell(new Cell(2, 1, c_zero));
        Assert.AreEqual(m_field.GetCell(0, 0).Role, c_cross);
        Assert.AreEqual(m_field.GetCell(1, 1).Role, c_cross);
        Assert.AreEqual(m_field.GetCell(2, 2).Role, c_cross);
        Assert.AreEqual(m_field.GetCell(2, 1).Role, c_zero);
        m_field.Print();
    }
    [Test]
    public void FieldTests_AddColumnsLeft_1()
    {
        m_field.AddColumnsLeft(1);
        Vector2Int new_size = new(4, 3);
        Vector2Int new_start_point = new(-1, 0);
        Assert.AreEqual(new_size, m_field.Size);
        Assert.AreEqual(new_start_point, m_field.StartPoint);
        m_field.Print();
    }
    [Test]
    public void FieldTests_AddColumnsLeft_5()
    {
        m_field.AddColumnsLeft(5);
        Vector2Int new_size = new(8, 3);
        Vector2Int new_start_point = new(-5, 0);
        Assert.AreEqual(new_size, m_field.Size);
        Assert.AreEqual(new_start_point, m_field.StartPoint);
        m_field.Print();
    }
    private readonly Field m_field = new(3, 3);
    private const string c_cross = "cross";
    private const string c_zero = "zero";
    private const string c_null = "null";
}
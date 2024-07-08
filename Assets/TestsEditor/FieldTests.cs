using NUnit.Framework;
using UnityEngine;
using Assets.TictactoeLogic.Scripts;

public class FieldTests
{
    private readonly GameSettings gameSettings = Resources.Load<GameSettings>("GameSettingsDefault");
    private Field field;
    [Test]
    public void Field_Constructor()
    {
        field = new(gameSettings);
        Assert.That(field.Size, Is.EqualTo(gameSettings.startFieldSize));
        Assert.AreEqual(field.Cells[0, 0].Role, Role.None);
    }
    [Test]
    public void Field_Set_Something()
    {
        field = new(gameSettings);
        Assert.IsTrue(field.SetCross(0, 0));
        Assert.IsTrue(field.SetZero(1, 1));
        Assert.IsFalse(field.SetZero(1, 1));
    }
    [Test]
    public void EnlargeField_Empty()
    {
        field = new(gameSettings);
        Debug.Log(field);
        field.EnlargeField();
        Debug.Log(field);
    }
    [Test]
    public void EnlargeField_SaveRoles()
    {
        field = new(gameSettings);
        field.SetCross(0, 0);
        field.SetZero(1, 1);
        field.SetCross(2, 2);
        Debug.Log(field);
        field.EnlargeField();
        Debug.Log(field);
        Assert.AreEqual(field.Cells[1, 1].Role, Role.Cross);
        Assert.AreEqual(field.Cells[2, 2].Role, Role.Zero);
        Assert.AreEqual(field.Cells[3, 3].Role, Role.Cross);
    }
    [Test]
    public void EnlargeField_SaveIndices()
    {
        field = new(gameSettings);
        field.SetCross(0, 0);
        field.SetZero(1, 1);
        field.SetCross(2, 2);
        Debug.Log(field);
        field.EnlargeField();
        Debug.Log(field);
        Assert.AreEqual(field.Cells[1, 1].x, 1);
        Assert.AreEqual(field.Cells[1, 1].y, 1);
    }
    [Test]
    public void WinCheck_EmptyField()
    {
        field = new(gameSettings);
        Assert.True(field.WinCheck(1, 1));

    }
    [Test]
    public void WinCheck_Horizontal()
    {
        field = new(gameSettings);
        field.EnlargeField();
        field.SetCross(1, 0);
        field.SetCross(1, 1);
        field.SetCross(1, 2);
        Debug.Log(field);
        Assert.True(field.WinCheck(1, 1));
    }
    [Test]
    public void WinCheck_Vertical()
    {
        field = new(gameSettings);
        field.EnlargeField();
        field.SetCross(0, 0);
        field.SetCross(1, 0);
        field.SetCross(2, 0);
        Debug.Log(field);
        Assert.True(field.WinCheck(1, 1));
    }
    [Test]
    public void WinCheck_EnlargeField_Once_CrossDiagonal()
    {
        field = new(gameSettings);
        field.EnlargeField();
        field.SetCross(1, 1);
        field.SetCross(2, 2);
        field.SetCross(3, 3);
        Debug.Log(field);
        Assert.True(field.WinCheck(3, 3));
    }
    [Test]
    public void WinCheck_EnlargeField_x3_CrossDiagonal()
    {
        field = new(gameSettings);
        field.SetCross(0, 0);
        field.SetCross(1, 1);
        field.SetCross(2, 2);
        field.EnlargeField();
        field.EnlargeField();
        field.EnlargeField();
        Debug.Log(field);
        Assert.True(field.WinCheck(2, 2));
    }
    [Test]
    public void WinCheck_EnlargeField_Once_CrossAntiDiagonal()
    {
        field = new(gameSettings);
        field.EnlargeField();
        field.SetCross(1, 3);
        field.SetCross(2, 2);
        field.SetCross(3, 1);
        Debug.Log(field);
        Assert.True(field.WinCheck(3, 3));
    }
}

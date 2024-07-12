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
    }
    [Test]
    public void Field_Set_Something()
    {
        field = new(gameSettings);
        Assert.IsTrue(field.MakeMove(1, 1, gameSettings.roles[1]));
        Assert.IsTrue(field.MakeMove(0, 0, gameSettings.roles[0]));
        Assert.IsFalse(field.MakeMove(0, 0, gameSettings.roles[1]));
        Assert.IsFalse(field.MakeMove(0, 0, gameSettings.roles[0]));
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
        field.MakeMove(0, 0, gameSettings.roles[0]);
        field.MakeMove(1, 1, gameSettings.roles[1]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
        Debug.Log(field);
        field.EnlargeField();
        Debug.Log(field);
        Assert.AreEqual(field.Cells[1, 1].Role, gameSettings.roles[0]);
        Assert.AreEqual(field.Cells[2, 2].Role, gameSettings.roles[1]);
        Assert.AreEqual(field.Cells[3, 3].Role, gameSettings.roles[0]);
    }
    [Test]
    public void EnlargeField_SaveIndices()
    {
        field = new(gameSettings);
        field.MakeMove(0, 0, gameSettings.roles[0]);
        field.MakeMove(1, 1, gameSettings.roles[1]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
        Debug.Log(field);
        field.EnlargeField();
        Debug.Log(field);
        Assert.AreEqual(field.Cells[1, 1].X, 1);
        Assert.AreEqual(field.Cells[1, 1].Y, 1);
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
        field.MakeMove(1, 0, gameSettings.roles[0]);
        field.MakeMove(1, 1, gameSettings.roles[0]);
        field.MakeMove(1, 2, gameSettings.roles[0]);
        Debug.Log(field);
        Assert.True(field.WinCheck(1, 1));
    }
    [Test]
    public void WinCheck_Vertical()
    {
        field = new(gameSettings);
        field.EnlargeField();
        field.MakeMove(0, 0, gameSettings.roles[0]);
        field.MakeMove(1, 0, gameSettings.roles[0]);
        field.MakeMove(2, 0, gameSettings.roles[0]);
        Debug.Log(field);
        Assert.True(field.WinCheck(1, 1));
    }
    [Test]
    public void WinCheck_EnlargeField_Once_CrossDiagonal()
    {
        field = new(gameSettings);
        field.EnlargeField();
        field.MakeMove(1, 1, gameSettings.roles[0]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
        field.MakeMove(3, 3, gameSettings.roles[0]);
        Debug.Log(field);
        Assert.True(field.WinCheck(3, 3));
    }
    [Test]
    public void WinCheck_EnlargeField_x3_CrossDiagonal()
    {
        field = new(gameSettings);
        field.MakeMove(0, 0, gameSettings.roles[0]);
        field.MakeMove(1, 1, gameSettings.roles[0]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
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
        field.MakeMove(0, 3, gameSettings.roles[0]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
        field.MakeMove(3, 1, gameSettings.roles[0]);
        Debug.Log(field);
        Assert.True(field.WinCheck(3, 3));
    }
}

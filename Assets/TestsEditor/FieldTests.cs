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
        field = new(gameSettings.startFieldSize);
        Assert.That(field.Dimention, Is.EqualTo(gameSettings.startFieldSize));
    }
    [Test]
    public void Field_Set_Something()
    {
        field = new(gameSettings.startFieldSize);
        Assert.IsTrue(field.MakeMove(1, 1, gameSettings.roles[1]));
        Assert.IsTrue(field.MakeMove(0, 0, gameSettings.roles[0]));
        Assert.IsFalse(field.MakeMove(0, 0, gameSettings.roles[1]));
        Assert.IsFalse(field.MakeMove(0, 0, gameSettings.roles[0]));
    }
    [Test]
    public void EnlargeField_Empty()
    {
        field = new(gameSettings.startFieldSize);
        Debug.Log(field);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        Debug.Log(field);
    }
    [Test]
    public void EnlargeField_SaveRoles()
    {
        field = new(gameSettings.startFieldSize);
        field.MakeMove(0, 0, gameSettings.roles[0]);
        field.MakeMove(1, 1, gameSettings.roles[1]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
        Debug.Log(field);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        Debug.Log(field);
        Assert.AreEqual(field.Cells[1, 1].Role, gameSettings.roles[0]);
        Assert.AreEqual(field.Cells[2, 2].Role, gameSettings.roles[1]);
        Assert.AreEqual(field.Cells[3, 3].Role, gameSettings.roles[0]);
    }
    [Test]
    public void EnlargeField_SaveIndices()
    {
        field = new(gameSettings.startFieldSize);
        field.MakeMove(0, 0, gameSettings.roles[0]);
        field.MakeMove(1, 1, gameSettings.roles[1]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
        Debug.Log(field);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        Debug.Log(field);
        Assert.AreEqual(field.Cells[1, 1].X, 1);
        Assert.AreEqual(field.Cells[1, 1].Y, 1);
    }
    [Test]
    public void WinCheck_EmptyField()
    {
        field = new(gameSettings.startFieldSize);
        Assert.True(field.WinCheck(gameSettings.winlineSize, 1, 1));

    }
    [Test]
    public void WinCheck_Horizontal()
    {
        field = new(gameSettings.startFieldSize);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        field.MakeMove(1, 0, gameSettings.roles[0]);
        field.MakeMove(1, 1, gameSettings.roles[0]);
        field.MakeMove(1, 2, gameSettings.roles[0]);
        Debug.Log(field);
        Assert.True(field.WinCheck(gameSettings.winlineSize, 1, 1));
    }
    [Test]
    public void WinCheck_Vertical()
    {
        field = new(gameSettings.startFieldSize);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        field.MakeMove(0, 0, gameSettings.roles[0]);
        field.MakeMove(1, 0, gameSettings.roles[0]);
        field.MakeMove(2, 0, gameSettings.roles[0]);
        Debug.Log(field);
        Assert.True(field.WinCheck(gameSettings.winlineSize, 1, 1));
    }
    [Test]
    public void WinCheck_EnlargeField_Once_CrossDiagonal()
    {
        field = new(gameSettings.startFieldSize);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        field.MakeMove(1, 1, gameSettings.roles[0]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
        field.MakeMove(3, 3, gameSettings.roles[0]);
        Debug.Log(field);
        Assert.True(field.WinCheck(gameSettings.winlineSize, 3, 3));
    }
    [Test]
    public void WinCheck_EnlargeField_x3_CrossDiagonal()
    {
        field = new(gameSettings.startFieldSize);
        field.MakeMove(0, 0, gameSettings.roles[0]);
        field.MakeMove(1, 1, gameSettings.roles[0]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        Debug.Log(field);
        Assert.True(field.WinCheck(gameSettings.winlineSize, 2, 2));
    }
    [Test]
    public void WinCheck_EnlargeField_Once_CrossAntiDiagonal()
    {
        field = new(gameSettings.startFieldSize);
        field.EnlargeField(gameSettings.enlargeFieldStep);
        field.MakeMove(1, 3, gameSettings.roles[0]);
        field.MakeMove(2, 2, gameSettings.roles[0]);
        field.MakeMove(3, 1, gameSettings.roles[0]);
        Debug.Log(field);
        Assert.True(field.WinCheck(gameSettings.winlineSize, 2, 2));
    }
}

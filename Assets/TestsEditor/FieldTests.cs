using NUnit.Framework;
using UnityEngine;
using Assets.TictactoeLogic.Scripts;

public class FieldTests
{
    private readonly GameSettings m_gameSettings = Resources.Load<GameSettings>("GameSettingsDefault");
    private readonly GameSettingsRoles m_gameRoles = Resources.Load<GameSettingsRoles>("Roles");
    private Field m_field;
    [Test]
    public void Field_Constructor()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        Assert.That(m_field.Dimention, Is.EqualTo(m_gameSettings.startFieldSize));
    }
    [Test]
    public void Field_Set_Something()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        Assert.IsTrue(m_field.MakeMove(1, 1, m_gameRoles.roles[1]));
        Assert.IsTrue(m_field.MakeMove(0, 0, m_gameRoles.roles[0]));
        Assert.IsFalse(m_field.MakeMove(0, 0, m_gameRoles.roles[1]));
        Assert.IsFalse(m_field.MakeMove(0, 0, m_gameRoles.roles[0]));
    }
    [Test]
    public void EnlargeField_Empty()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        Debug.Log(m_field);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        Debug.Log(m_field);
    }
    [Test]
    public void EnlargeField_SaveRoles()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        m_field.MakeMove(0, 0, m_gameRoles.roles[0]);
        m_field.MakeMove(1, 1, m_gameRoles.roles[1]);
        m_field.MakeMove(2, 2, m_gameRoles.roles[0]);
        Debug.Log(m_field);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        Debug.Log(m_field);
        Assert.AreEqual(m_field.Cells[1, 1].Role, m_gameRoles.roles[0]);
        Assert.AreEqual(m_field.Cells[2, 2].Role, m_gameRoles.roles[1]);
        Assert.AreEqual(m_field.Cells[3, 3].Role, m_gameRoles.roles[0]);
    }
    [Test]
    public void EnlargeField_SaveIndices()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        m_field.MakeMove(0, 0, m_gameRoles.roles[0]);
        m_field.MakeMove(1, 1, m_gameRoles.roles[1]);
        m_field.MakeMove(2, 2, m_gameRoles.roles[0]);
        Debug.Log(m_field);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        Debug.Log(m_field);
        Assert.AreEqual(m_field.Cells[1, 1].X, 1);
        Assert.AreEqual(m_field.Cells[1, 1].Y, 1);
    }
    [Test]
    public void WinCheck_EmptyField()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        Assert.True(m_field.WinCheck(1, 1));

    }
    [Test]
    public void WinCheck_Horizontal()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        m_field.MakeMove(1, 0, m_gameRoles.roles[0]);
        m_field.MakeMove(1, 1, m_gameRoles.roles[0]);
        m_field.MakeMove(1, 2, m_gameRoles.roles[0]);
        Debug.Log(m_field);
        Assert.True(m_field.WinCheck(1, 1));
    }
    [Test]
    public void WinCheck_Vertical()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        m_field.MakeMove(0, 0, m_gameRoles.roles[0]);
        m_field.MakeMove(1, 0, m_gameRoles.roles[0]);
        m_field.MakeMove(2, 0, m_gameRoles.roles[0]);
        Debug.Log(m_field);
        Assert.True(m_field.WinCheck(1, 1));
    }
    [Test]
    public void WinCheck_EnlargeField_Once_CrossDiagonal()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        m_field.MakeMove(1, 1, m_gameRoles.roles[0]);
        m_field.MakeMove(2, 2, m_gameRoles.roles[0]);
        m_field.MakeMove(3, 3, m_gameRoles.roles[0]);
        Debug.Log(m_field);
        Assert.True(m_field.WinCheck(3, 3));
    }
    [Test]
    public void WinCheck_EnlargeField_x3_CrossDiagonal()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        m_field.MakeMove(0, 0, m_gameRoles.roles[0]);
        m_field.MakeMove(1, 1, m_gameRoles.roles[0]);
        m_field.MakeMove(2, 2, m_gameRoles.roles[0]);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        Debug.Log(m_field);
        Assert.True(m_field.WinCheck(2, 2));
    }
    [Test]
    public void WinCheck_EnlargeField_Once_CrossAntiDiagonal()
    {
        m_field = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        m_field.EnlargeField(m_gameSettings.enlargeFieldStep);
        m_field.MakeMove(1, 3, m_gameRoles.roles[0]);
        m_field.MakeMove(2, 2, m_gameRoles.roles[0]);
        m_field.MakeMove(3, 1, m_gameRoles.roles[0]);
        Debug.Log(m_field);
        Assert.True(m_field.WinCheck(2, 2));
    }
}

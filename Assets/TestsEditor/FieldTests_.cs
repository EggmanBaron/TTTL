using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.TictactoeLogic.Scripts;

public class FieldTests_
{
    private Field m_field = new(3, 3);
    [Test]
    public void FieldConstructorNonuniform()
    {
        var height = 3;
        var width = 3;
        Field field = new(height, width);
    }
    [Test]
    public void FieldConstructorUniform()
    {
        var size = 3;
        Field field = new(size, size);
    }
    [Test]
    public void FieldGetCell()
    {

    }
}

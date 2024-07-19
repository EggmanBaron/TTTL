using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CellViewTests
{
    [UnityTest]
    public IEnumerator CellViewTestsAwake()
    {
        GameObject goCellView = new("cell");
        CellView cellView = goCellView.AddComponent<CellView>();
        Assert.IsNotNull(cellView);
        yield return null;
    }
}

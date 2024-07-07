using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;

public class ExampleTest
{
    [Test]
    public void SimpleTest()
    {
        Assert.AreEqual(1, 1);
    }

    [UnityTest]
    public IEnumerator CoroutineTest()
    {
        yield return null; // Пропускаем один кадр
        Assert.AreEqual(1, 1);
    }
}

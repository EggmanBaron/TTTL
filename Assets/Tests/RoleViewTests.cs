using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.TestTools;

public class RoleViewTests
{
    public static GameObject goRoleView;
    [UnityTest]
    public IEnumerator RoleViewTestsAwake()
    {
        GameObject goRoleView = new("role");
        RoleView roleView = goRoleView.AddComponent<RoleView>();
        Assert.IsNotNull(roleView);
        Assert.IsFalse(roleView.gameObject.activeInHierarchy);
        yield return null;
    }
    [UnityTest]
    public IEnumerator RoleViewTestsActivate()
    {
        GameObject goRoleView = new("role");
        RoleView roleView = goRoleView.AddComponent<RoleView>();
        roleView.Activate();
        Assert.IsTrue(roleView.gameObject.activeInHierarchy);
        yield return null;
    }
}

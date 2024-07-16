using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.TictactoeLogic.Scripts;
using UnityEngine;

public class CellView : MonoBehaviour
{
    private void Awake()
    {
        RoleViews = GetComponentsInChildren<RoleView>();
        GameObject[] goRoleViews = RoleViews.Select(goRoleView => goRoleView.gameObject).ToArray();
        foreach (GameObject gameObject in goRoleViews) { gameObject.SetActive(false); }
    }
    private void Show(string roleName)
    {
        foreach (RoleView roleView in RoleViews)
        {
            if (roleView.Role == roleName) { roleView.gameObject.SetActive(true); }
        }
    }
    public Vector3 Size
    {
        get
        {
            Vector3 result = new();
            List<Vector3> sizes = new();
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in renderers) { sizes.Add(rend.bounds.size); }
            foreach (Vector3 size in sizes) { result = (size.magnitude > result.magnitude) ? size : result; }
            return result;
        }
    }
    public int X { get; set; }
    public int Y { get; set; }
    public string Role
    {
        get => role;
        set
        {
            role = value;
            Show(value);
        }
    }
    private string role;
    private RoleView[] RoleViews { get; set; }
}

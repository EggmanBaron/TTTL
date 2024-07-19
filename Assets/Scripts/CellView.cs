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
            foreach (RoleView roleView in RoleViews)
            {
                if (roleView.Role != value) { roleView.Activate(); }
            }
            role = value;
        }
    }
    private string role;
    private RoleView[] RoleViews { get; set; }
}

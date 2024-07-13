using System.Collections;
using System.Collections.Generic;
using Assets.TictactoeLogic.Scripts;
using UnityEngine;

public class CellView : MonoBehaviour
{
    public void Show(string roleName)
    {

    }
    public Vector3 CellSize
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
        set => role ??= value;
    }
    private string role;
    private Vector3 size;
    public Dictionary<string, GameObject> roles;
}

using UnityEngine;

public class CellInput : MonoBehaviour
{
    public delegate void ClickHandler(CellInput sender);
    public event ClickHandler Click;
    protected virtual void OnClick()
    {
        Click?.Invoke(this);
    }
    private void OnMouseDown()
    {
        OnClick();
    }
    public int X { get { return GetComponent<CellView>().X; } }
    public int Y { get { return GetComponent<CellView>().Y; } }
}

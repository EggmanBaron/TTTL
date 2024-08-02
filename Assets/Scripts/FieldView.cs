using System;
using Assets.TictactoeLogic.Scripts;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    public void CreateField()
    {
        int dimention = m_fieldModel.Dimention;
        CellViews = new GameObject[dimention, dimention];
        foreach (Cell cell in m_fieldModel.Cells)
        {
            GameObject gameObject = Instantiate(m_cell);
            CellView cellView = gameObject.GetComponent<CellView>();
            cellView.X = cell.X;
            cellView.Y = cell.Y;
            if (cellView.Role != null) { cellView.Role = cell.Role; }
            CellViews[cell.X, cell.Y] = gameObject;
        }

        var cellSize = m_cell.GetComponent<CellView>().Size;
        float x = -cellSize.x * dimention / 2;
        float z = cellSize.z * dimention / 2;
        Vector3 firstCellPosition = new() { x = x, y = 0, z = z };
        Vector3 position = firstCellPosition;
        for (int i = 0; i < dimention; i++)
        {
            for (int j = 0; j < dimention; j++)
            {
                CellViews[i, j].transform.position = position;
                position.z -= cellSize.z;
            }
            position.x += cellSize.x;
            position.z = firstCellPosition.z;
        }
    }
    public void WinAction()
    {
        Debug.Log(string.Format("{0} Win!", m_fieldModel.LastChanged.Role));
    }
    public void UpdateFieldView()
    {
        int dimention = CellViews.GetLength(0);
        if (dimention != m_fieldModel.Dimention)
        {
            int increment = m_fieldModel.Dimention - dimention;
            Enlarge(increment);
        }
        int x = m_fieldModel.LastChanged.X;
        int y = m_fieldModel.LastChanged.Y;
        string role = m_fieldModel.LastChanged.Role;
        CellView cell = CellViews[x, y].GetComponent<CellView>();
        cell.Role = role;
    }

    private void Enlarge(int increment)
    {
        throw new NotImplementedException();
    }
    private GameObject[,] CellViews { get; set; }
    public Field FieldModel { set { m_fieldModel = value; } }
    [SerializeField]
    private GameObject m_cell;
    private Field m_fieldModel;
}
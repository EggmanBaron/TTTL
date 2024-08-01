using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Assets.TictactoeLogic.Scripts;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    public void CreateField()
    {
        int dimention = fieldModel.Cells.GetLength(0);
        GameObject[,] gameObjects = new GameObject[dimention, dimention];
        CellViews = gameObjects;
        foreach (Cell cell in fieldModel.Cells)
        {
            GameObject gameObject = Instantiate(this.cell);
            CellView cellView = gameObject.GetComponent<CellView>();
            cellView.X = cell.X;
            cellView.Y = cell.Y;
            if (cellView.Role != null) { cellView.Role = cell.Role; }
            gameObjects[cell.X, cell.Y] = gameObject;
        }

        var cellSize = cell.GetComponent<CellView>().Size;
        float x = -cellSize.x * dimention / 2;
        float z = cellSize.z * dimention / 2;
        Vector3 firstCellPosition = new() { x = x, y = 0, z = z };
        Vector3 position = firstCellPosition;
        for (int i = 0; i < dimention; i++)
        {
            for (int j = 0; j < dimention; j++)
            {
                gameObjects[i, j].transform.position = position;
                position.z -= cellSize.z;
            }
            position.x += cellSize.x;
            position.z = firstCellPosition.z;
        }
    }
    public void WinAction()
    {
        Debug.Log(string.Format("{0} Win!", fieldModel.LastChanged.Role));
    }
    public void UpdateFieldView()
    {
        int dimention = CellViews.GetLength(0);
        if (dimention != fieldModel.Dimention)
        {
            int increment = fieldModel.Dimention - dimention;
            Enlarge(increment);
        }
        int x = fieldModel.LastChanged.X;
        int y = fieldModel.LastChanged.Y;
        string role = fieldModel.LastChanged.Role;
        CellView cell = CellViews[x, y].GetComponent<CellView>();
        cell.Role = role;
    }

    private void Enlarge(int increment)
    {
        throw new NotImplementedException();
    }
    private GameObject[,] CellViews { get; set; }
    public GameObject cell;
    public Field fieldModel;
}
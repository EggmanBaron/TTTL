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
    public void UpdateFieldView()
    {
        if (Dimention != fieldModel.Dimention)
        {
            int increment = fieldModel.Dimention - Dimention;
            Enlarge(increment);
        }
        int x = fieldModel.LastChanged.X;
        int y = fieldModel.LastChanged.Y;
        CellView cell = CellViews[x, y].GetComponent<CellView>();
        cell.Role = fieldModel.LastChanged.Role;
    }

    private void Enlarge(int increment)
    {
        throw new NotImplementedException();
    }

    private int Dimention { get => CellViews.GetLength(0); }
    private GameObject[,] CellViews { get; set; }
    public GameObject cell;
    public Field fieldModel;
}
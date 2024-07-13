using System.Collections;
using System.Collections.Generic;
using Assets.TictactoeLogic.Scripts;
using UnityEngine;

public class FieldView : MonoBehaviour
{
    void Start()
    {
        gameSettings = FindAnyObjectByType<GameSettings>();
    }
    public void CreateField()
    {
        int cellCount = fieldModel.Cells.GetLength(0);
        GameObject[,] gameObjects = new GameObject[cellCount, cellCount];
        foreach (Cell cell in fieldModel.Cells)
        {
            GameObject gameObject = Instantiate(this.cell);
            CellView cellView = gameObject.GetComponent<CellView>();
            cellView.X = cell.X;
            cellView.Y = cell.Y;
            cellView.Role = cell.Role;
            gameObjects[cell.X, cell.Y] = gameObject;
        }

        var cellSize = cell.GetComponent<CellView>().CellSize;
        float x = -cellSize.x * cellCount / 2;
        float z = cellSize.z * cellCount / 2;
        Vector3 firstCellPosition = new() { x = x, y = 0, z = z };
        Vector3 position = firstCellPosition;
        for (int i = 0; i < cellCount; i++)
        {
            for (int j = 0; j < cellCount; j++)
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
        foreach (Cell cell in fieldModel.Cells)
        {
            foreach (CellView cellView in cellViews)
            {

            }
            // CellView cellView = Instantiate(goCellView).GetComponent<CellView>();
        }
    }
    private List<CellView> cellViews = new();
    public GameObject cell;
    public GameObject cross;
    public GameObject zero;
    public Field fieldModel;
    private GameSettings gameSettings;
}
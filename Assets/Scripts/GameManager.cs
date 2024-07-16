using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.TictactoeLogic.Scripts;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        fieldModel = new(gameSettings.startFieldSize);
        FieldView fieldView = Instantiate(goFieldView).GetComponent<FieldView>();
        fieldView.fieldModel = fieldModel;
        fieldView.CreateField();
        FindAnyObjectByType<CellInput>();
        foreach (CellInput cellInput in CellInputs)
        {
            cellInput.Click += MakeMove;
        }
    }
    private void MakeMove(object sender)
    {
        Debug.Log("MakeMove!");
    }
    private CellInput[] CellInputs
    {
        get { return FindObjectsByType<CellInput>(FindObjectsSortMode.None); }
    }
    public GameSettings gameSettings;
    public GameObject goFieldView;
    private Field fieldModel;
}

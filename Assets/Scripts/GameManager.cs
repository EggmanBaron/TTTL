using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.TictactoeLogic.Scripts;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        fieldModel = new(gameSettings);
        fieldView.fieldModel = fieldModel;
        fieldView.CreateField();
    }
    public GameSettings gameSettings;
    public FieldView fieldView;
    private Field fieldModel;
}

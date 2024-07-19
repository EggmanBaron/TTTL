using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.TictactoeLogic.Scripts;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        playerManager = new(gameRoles.roles);
        fieldModel = new(gameSettings.startFieldSize);
        fieldView = Instantiate(goFieldView).GetComponent<FieldView>();
        fieldView.fieldModel = fieldModel;
        fieldView.CreateField();
        FindAnyObjectByType<CellInput>();
        foreach (CellInput cellInput in CellInputs)
        {
            cellInput.Click += MakeMove;
        }
    }
    private void MakeMove(CellInput sender)
    {

        if (fieldModel.MakeMove(sender.X, sender.Y, playerManager.GetActivePlayer().Role))
        {
            fieldView.UpdateFieldView();
        }
    }
    private CellInput[] CellInputs
    {
        get { return FindObjectsByType<CellInput>(FindObjectsSortMode.None); }
    }
    public GameSettings gameSettings;
    public GameSettingsRoles gameRoles;
    public GameObject goFieldView;
    private Field fieldModel;
    private FieldView fieldView;
    private PlayerManager playerManager;
    private Player CurrentPlayer { get; set; }
    private readonly List<Player> players = new();
    private Player player1;
    private Player player2;
}

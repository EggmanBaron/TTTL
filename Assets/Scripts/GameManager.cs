using UnityEngine;
using Assets.TictactoeLogic.Scripts;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        m_playerManager = new(gameRoles.roles);
        m_fieldModel = new(gameSettings.startFieldSize, gameSettings.winlineSize);
        m_fieldView = Instantiate(goFieldView).GetComponent<FieldView>();
        m_fieldView.fieldModel = m_fieldModel;
        m_fieldView.CreateField();
        m_cellInputs = FindObjectsByType<CellInput>(FindObjectsSortMode.None);
        foreach (CellInput cellInput in m_cellInputs)
        {
            cellInput.Click += MakeMove;
        }
    }
    private void MakeMove(CellInput sender)
    {
        int winlineSize = gameSettings.winlineSize;
        int x = sender.X;
        int y = sender.Y;
        if (m_fieldModel.MakeMove(x, y, m_playerManager.GetActivePlayer().Role))
        {
            m_fieldView.UpdateFieldView();
            m_fieldModel.WinCheck(x, y);
        }
    }
    public GameSettings gameSettings;
    public GameSettingsRoles gameRoles;
    public GameObject goFieldView;
    private CellInput[] m_cellInputs;
    private Field m_fieldModel;
    private FieldView m_fieldView;
    private PlayerManager m_playerManager;
}

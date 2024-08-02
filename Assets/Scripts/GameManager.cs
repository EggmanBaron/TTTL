using UnityEngine;
using Assets.TictactoeLogic.Scripts;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        m_playerManager = new(m_gameRoles.roles);
        m_fieldModel = new(m_gameSettings.startFieldSize, m_gameSettings.winlineSize);
        m_fieldView = Instantiate(m_goFieldView).GetComponent<FieldView>();
        m_fieldView.FieldModel = m_fieldModel;
        m_fieldView.CreateField();
        m_cellInputs = FindObjectsByType<CellInput>(FindObjectsSortMode.None);
        foreach (CellInput cellInput in m_cellInputs)
        {
            cellInput.Click += MakeMove;
        }
    }
    private void MakeMove(CellInput sender)
    {
        int x = sender.X;
        int y = sender.Y;
        string role = m_playerManager.ActivePlayer.Role;
        bool moveResult = m_fieldModel.MakeMove(x, y, role);
        if (moveResult)
        {
            bool weHaveaWinner = m_fieldModel.WinCheck(x, y);
            m_playerManager.ActivePlayer.IsWinner = weHaveaWinner;
            if (weHaveaWinner)
            {
                foreach (CellInput cellInput in m_cellInputs)
                {
                    cellInput.Click -= MakeMove;
                }
                m_fieldView.WinAction();
            }
            else if (m_fieldModel.IsFull())
            {
                m_fieldModel.EnlargeField(m_gameSettings.enlargeFieldStep);
            }
        }
        m_fieldView.UpdateFieldView();
        m_playerManager.ActivateNextPlayer();
    }
    [SerializeField]
    private GameSettings m_gameSettings;
    [SerializeField]
    private GameSettingsRoles m_gameRoles;
    [SerializeField]
    private GameObject m_goFieldView;
    private CellInput[] m_cellInputs;
    private Field m_fieldModel;
    private FieldView m_fieldView;
    private PlayerManager m_playerManager;
}

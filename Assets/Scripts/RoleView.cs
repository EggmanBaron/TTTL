using UnityEngine;

public class RoleView : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public string Role
    {
        get { return m_role; }
        set
        {
            if (!Application.isPlaying)
            {
                m_role = value;
            }
        }
    }
    public int RoleIndex
    {
        get { return gameRoles.roles.IndexOf(Role); }
        set { }
    }
    public GameSettingsRoles gameRoles;
    [SerializeField] private string m_role;
}

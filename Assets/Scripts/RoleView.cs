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
        get { return _role; }
        set
        {
            if (!Application.isPlaying)
            {
                _role = value;
            }
        }
    }
    public int RoleIndex
    {
        get { return gameRoles.roles.IndexOf(Role); }
        set { }
    }
    public GameSettingsRoles gameRoles;
    [SerializeField] private string _role;
}

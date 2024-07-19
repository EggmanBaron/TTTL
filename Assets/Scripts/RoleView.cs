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
        get { return role; }
        set
        {
            if (!Application.isPlaying)
            {
                role = value;
            }
        }
    }
    public int RoleIndex
    {
        get { return gameRoles.roles.IndexOf(Role); }
    }
    public GameSettingsRoles gameRoles;
    private string role;
}

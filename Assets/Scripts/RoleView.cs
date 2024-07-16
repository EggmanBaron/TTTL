using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoleView : MonoBehaviour
{
    public string Role { get; set; }
    public int RoleIndex
    {
        get { return gameRoles.roles.IndexOf(Role); }
    }
    public GameSettingsRoles gameRoles;
}

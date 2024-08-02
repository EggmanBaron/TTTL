using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Tictactoe/Game Roles", order = 51)]
public class GameSettingsRoles : ScriptableObject
{
    [Header("Game Roles")]
    [Tooltip("Roles, cross, zero, etc.")]
    public List<string> roles;
}

using System.Collections.Generic;
using UnityEngine;
namespace Assets.TictactoeLogic.Scripts
{
    [CreateAssetMenu(fileName = "NewGameSettings", menuName = "TictactoeLogic/Game Settings", order = 51)]
    public class GameSettings : ScriptableObject
    {
        [Header("Game Field Settings")]
        [Tooltip("Number of cells in rows and columns when the game starts")]
        public int startFieldSize;
        [Tooltip("When game field is full, each side enlarges by this number of cells")]
        public int enlargeFieldStep;
        [Tooltip("Size of winline")]
        public int winlineSize;
        [Tooltip("Roles, cross, zero, etc.")]
        public List<string> roles;
    }
}

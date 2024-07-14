using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Tictactoe/Game Settings", order = 51)]
public class GameSettings : ScriptableObject
{
    [Header("Game Field Settings")]
    [Tooltip("Number of cells in rows and columns when the game starts")]
    public int startFieldSize;
    [Tooltip("When game field is full, each side enlarges by this number of cells")]
    public int enlargeFieldStep;
    [Tooltip("Size of winline")]
    public int winlineSize;
}

using System;
using UnityEngine;
namespace Assets.Scripts.GameLogic
{
    public class GameLogic
    {
        public GameLogic(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _field = new(gameSettings);
        }
        public void MakeMove(int x, int y, Player.Role role)
        {
        }
        private void UpdateField(int fieldSize)
        {
            throw new NotImplementedException();
        }
        private readonly GameSettings _gameSettings;
        private Field _field;
    }
}
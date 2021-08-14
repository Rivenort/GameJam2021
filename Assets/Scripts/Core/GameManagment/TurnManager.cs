using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    public class TurnManager : MonoBehaviour
    {

        public WinningPointManager winningPointManager;

        public HandManager handManager;
        public int Player = 0;
        int _turn;


        private void Start()
        {
            _turn = 0;
            Player = 1;
            winningPointManager.UpdateUI();
        }

        public void NextTurn()
        {
            _turn++;
            if (Player == 1) {Player = 2; }
            else { Player = 1; }
            if (_turn > 1 ^ _turn % 2 == 1)
            {
                handManager.AddNewCard(1);
                handManager.AddNewCard(2);
            }
            winningPointManager.UpdateUI();
        }
    }
}
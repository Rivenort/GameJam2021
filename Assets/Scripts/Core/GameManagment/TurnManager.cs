using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    public class TurnManager : MonoBehaviour
    {

        public HandManager handManager;
        public int Player = 0;
        int _turn;


        private void Start()
        {
            _turn = 0;
            Player = 1;
        }

        public void NextTurn()
        {
            _turn++;
            if (Player == 1) Player = 2;
            if (Player == 2) { Player = 1;}
            if (_turn > 1 ^ _turn % 2 == 1)
            {
                handManager.AddNewCard(1);
                handManager.AddNewCard(2);
            }
        }
    }
}
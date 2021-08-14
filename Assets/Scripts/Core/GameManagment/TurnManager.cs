using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BestGameEver
{
    public class TurnManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI Counter;

        public int ActionPoints=20;

        public WinningPointManager winningPointManager;

        public HandManager handManager;
        public int Player = 0;
        int _turn;


        public bool Action(int Value)
        {
            if (Value <= ActionPoints)
            {
                ActionPoints -= Value;
                Counter.text = ActionPoints.ToString();
                return true;
            }
            else { return false; }
        }

        private void Start()
        {
            _turn = 0;
            Player = 1;
            winningPointManager.UpdateUI();
        }

        public void NextTurn()
        {
            ActionPoints = 20;
            Counter.text = ActionPoints.ToString();

            if (Player == 1) {Player = 2; }
            else { Player = 1; }


            if (_turn > 0 & _turn % 2 == 1)
            {
                handManager.AddNewCard(1);
                handManager.AddNewCard(2);
            }
            winningPointManager.UpdateUI();
            _turn++;
        }
    }
}
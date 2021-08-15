using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BestGameEver 
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UI_ScreenNewTurn : MonoBehaviour
    {

        private bool m_wasFirstUpdate = false;
        public GameObject uiScreenDrawACard;
        public TMP_Text textNewTurn;
        public TMP_Text textPlayerName;
        public string textPlayer1 = "Gracz 1";
        public string textPlayer2 = "Gracz 2";
        public Color colorPlayer1;
        public Color colorPlayer2;

        void Awake()
        {
            
        }

        private void Update()
        {
            if (!m_wasFirstUpdate)
            {
                m_wasFirstUpdate = true;
                M_GamePlayManager.SAddListener_OnNewTurn(OnNewTurn);

                PlayerType player = M_GamePlayManager.SGetCurrentPlayer();
                if (player == PlayerType.PLAYER_ONE)
                {
                    textPlayerName.text = textPlayer1;
                    textPlayerName.color = colorPlayer1;
                    textNewTurn.color = colorPlayer1;
                }
                if (player == PlayerType.PLAYER_TWO)
                {
                    textPlayerName.text = textPlayer2;
                    textPlayerName.color = colorPlayer2;
                    textNewTurn.color = colorPlayer2;
                }
            }
        }



        public void OnNewTurn(PlayerType player)
        {
            gameObject.SetActive(true);
            if (player == PlayerType.PLAYER_ONE)
            {
                textPlayerName.text = textPlayer1;
                textPlayerName.color = colorPlayer1;
                textNewTurn.color = colorPlayer1;
            }
            if (player == PlayerType.PLAYER_TWO)
            {
                textPlayerName.text = textPlayer2;
                textPlayerName.color = colorPlayer2;
                textNewTurn.color = colorPlayer2;
            }
        }

        public void OnScreenFinished()
        {
            M_GamePlayManager.SScreenNewTurnFinished();
            if (M_CardManager.SCanDrawACard(M_GamePlayManager.SGetCurrentPlayer()))
                uiScreenDrawACard.SetActive(true);
        }
    }

}
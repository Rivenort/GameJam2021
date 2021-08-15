using System.Collections;
using System.Collections.Generic;
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

        void Awake()
        {
            
        }

        private void Update()
        {
            if (!m_wasFirstUpdate)
            {
                m_wasFirstUpdate = true;
                M_GamePlayManager.SAddListener_OnNewTurn(OnNewTurn);
            }
        }

        public void OnNewTurn(PlayerType player)
        {
            gameObject.SetActive(true);
        }

        public void OnScreenFinished()
        {
            M_GamePlayManager.SScreenNewTurnFinished();
            if (M_CardManager.SCanDrawACard(M_GamePlayManager.SGetCurrentPlayer()))
                uiScreenDrawACard.SetActive(true);
        }
    }

}
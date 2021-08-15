using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UI_Points : MonoBehaviour
    {
        public PlayerType playerType;
        public TMP_Text uiTextPoints;

        private bool m_wasFirstUpdate = false;



        private void Update()
        {
            if (!m_wasFirstUpdate)
            {
                M_GamePlayManager.SAddListener_OnPointsChange(OnPointsChanged);
                if (playerType == PlayerType.PLAYER_ONE)
                    uiTextPoints.text = M_GamePlayManager.SGetPlayer1Points().ToString();
                if (playerType == PlayerType.PLAYER_TWO)
                    uiTextPoints.text = M_GamePlayManager.SGetPlayer2Points().ToString();
                m_wasFirstUpdate = true;
            }
        }

        private void OnPointsChanged(PlayerType player, int points)
        {
            if (player == this.playerType)
            {
                uiTextPoints.text = points.ToString();
            }
        }
    }

}
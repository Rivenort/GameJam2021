using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UI_ScreenPhaseAction : MonoBehaviour
    {

        private bool m_wasFirstUpdate = false;


        private void Update()
        {
            if (!m_wasFirstUpdate)
            {
                m_wasFirstUpdate = true;
                M_GamePlayManager.SAddListener_OnActionPhase(OnActionPhase);
                gameObject.SetActive(false);
            }
        }

        private void OnActionPhase()
        {
            M_GamePlayManager.SShowCards();
            gameObject.SetActive(true);
        }
    }

}
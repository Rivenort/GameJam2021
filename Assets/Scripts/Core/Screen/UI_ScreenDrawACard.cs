using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UI_ScreenDrawACard : MonoBehaviour
    {
        public GameObject uiScreenPhaseAction;

        void Awake()
        {
           
        }

        private void OnEnable()
        {
            M_CardManager.SDrawRandCard();
        }

        public void OnScreenFinished()
        {
            M_GamePlayManager.SCallPhaseDrawCardEnded();
        }
        
    }

}
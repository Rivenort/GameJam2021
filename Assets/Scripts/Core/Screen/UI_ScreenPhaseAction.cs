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


        private void OnEnable()
        {
            M_GamePlayManager.SShowCards();
        }

    }

}
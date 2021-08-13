using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class MobUI : MonoBehaviour
    {
        public GameObject uiTopPanel;

        void Start()
        {
            if (uiTopPanel == null)
                throw new CE_ComponentNotFullyInitialized();
        }


        public void OnMobClick()
        {
            uiTopPanel.gameObject.SetActive(!uiTopPanel.gameObject.activeSelf);
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class UI_ButtonNextTurn : MonoBehaviour
    {

        private Button m_button;
        private bool m_wasFirstUpdate = false;

        void Start()
        {
            m_button = GetComponent<Button>();
            m_button.onClick.AddListener(() => {

                M_GamePlayManager.SNewTurn();
            });
        }

        private void Update()
        {
            if (!m_wasFirstUpdate)
            {
                m_wasFirstUpdate = true;
            }
        }
    }

}
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
        private static UI_ButtonNextTurn s_instance = null;

        private Button m_button;
        private bool m_wasFirstUpdate = false;

        void Start()
        {
            if (s_instance != null && s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;

            m_button = GetComponent<Button>();
            m_button.onClick.AddListener(() => {

                M_GamePlayManager.SNewTurn();
            });
        }

        public static void SSetInteractible(bool val)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_button.interactable = val;
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
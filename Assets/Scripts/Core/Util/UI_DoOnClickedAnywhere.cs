using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Error300.Util
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UI_DoOnClickedAnywhere : MonoBehaviour
    {
        public bool listenOnEnable;
        public UnityEvent doOnClick;

        private bool m_listening;
        [SerializeField]
        private bool m_waitForAnotherCall;

        private void OnEnable()
        {
            if (listenOnEnable)
                m_listening = true;
        }

        void Update()
        {
            if (m_listening)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                {
                    OnClicked();
                    if (m_waitForAnotherCall)
                        m_listening = false;
                }
            }
        }

        public void StartListening()
        {
            m_listening = true;
        }

        private void OnClicked()
        {
            m_listening = false;
            doOnClick?.Invoke();
        }
    }

}
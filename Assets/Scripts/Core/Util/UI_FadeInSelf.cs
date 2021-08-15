using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Error300.Util;

namespace WelcomeToMyCave
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UI_FadeInSelf : MonoBehaviour
    {
        public float time;

        private Color m_fromColor = new Color(1f, 1f, 1f, 0f);
        private Graphic[] m_graphics;
        private Color[] m_toColor;

        private float m_timeElapsed;
        private bool m_perform;

        public float delayAfter;
        private float m_delayTimeElapsed;

        [Header("Other")]
        public bool useUnscaledTime;
        public bool runOnEnable;

        public UnityEvent OnComplete;

        void Start()
        {
           
        }

        public void Run()
        {
            m_timeElapsed = 0f;
            m_delayTimeElapsed = 0f;
            m_perform = true;
        }

        private void OnEnable()
        {
            m_graphics = GetComponentsInChildren<Graphic>();
            if (m_graphics.Length == 0)
                Debug.LogWarning("Graphic component was not found.");
            else
            {
                m_toColor = new Color[m_graphics.Length];
                for (int i = 0; i < m_graphics.Length; i++)
                {
                    m_toColor[i] = m_graphics[i].color;
                    m_graphics[i].color = m_fromColor;
                }
            }

           
            if (runOnEnable)
                Run();
        }

        void Update()
        {
            if (m_perform)
                if (m_timeElapsed < time)
                {
                    for (int i = 0; i < m_graphics.Length; i++)
                    {
                        m_graphics[i].color = Color.Lerp(m_fromColor, m_toColor[i], m_timeElapsed / time);
                    }


                    if (!useUnscaledTime)
                        m_timeElapsed += Time.deltaTime;
                    else
                        m_timeElapsed += Time.unscaledDeltaTime;
                }
                else if (m_delayTimeElapsed >= delayAfter)
                {
                    OnComplete?.Invoke();
                    m_perform = true;
                }
                else
                {
                    if (!useUnscaledTime)
                        m_delayTimeElapsed += Time.deltaTime;
                    else
                        m_delayTimeElapsed += Time.unscaledDeltaTime;
                }
        }
    }

}
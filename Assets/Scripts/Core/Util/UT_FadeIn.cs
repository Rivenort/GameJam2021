using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Error300.Util
{
    public class UT_FadeIn : MonoBehaviour
    {
        public float duration;
        public UnityEvent onComplete;
        private float m_timeElapsed;
        private bool m_perform;
        private SpriteRenderer m_spriteRenderer;
        private Color m_fromColor;

        void Start()
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Run()
        {
            m_perform = true;
            m_fromColor = m_spriteRenderer.color;
            m_timeElapsed = 0f;
        }

        private void Update()
        {
            if (m_perform)
            {
                m_spriteRenderer.color = Color.Lerp(m_fromColor, Color.white, m_timeElapsed / duration);

                if (m_timeElapsed > duration)
                {
                    onComplete.Invoke();
                    m_perform = false;
                }
                m_timeElapsed += Time.deltaTime;
            }
        }
    }

}

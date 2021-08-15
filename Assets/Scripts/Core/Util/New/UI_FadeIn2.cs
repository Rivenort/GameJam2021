using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WelcomeToMyCave
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_FadeIn2 : MonoBehaviour
    {

        public float duration;
        public UnityEvent onComplete;
        public bool runOnEnable;
        private CanvasGroup m_group;

        public void Run()
        {
            m_group = GetComponent<CanvasGroup>();
            var setup = LeanTween.value(gameObject, OnUpdate, 0f, 1f, duration);
            setup.setOnComplete(() => {
                onComplete?.Invoke();
                m_group.alpha = 1f;
            });
        }

        void OnUpdate(float val)
        {
            m_group.alpha = val;
        }

        private void OnEnable()
        {
            if (runOnEnable)
            {
                Run();
            }
        }


    }

}
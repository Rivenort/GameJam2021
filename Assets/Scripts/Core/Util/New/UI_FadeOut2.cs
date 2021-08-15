using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_FadeOut2 : MonoBehaviour
    {

        public float duration;
        public UnityEvent onComplete;
        public bool runOnEnable;
        private CanvasGroup m_group;

        public void Run()
        {
            m_group = GetComponent<CanvasGroup>();
            var setup = LeanTween.value(gameObject, OnUpdate, 1f, 0f, duration);
            setup.setOnComplete(() => {
                onComplete?.Invoke();
                gameObject.SetActive(false);
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
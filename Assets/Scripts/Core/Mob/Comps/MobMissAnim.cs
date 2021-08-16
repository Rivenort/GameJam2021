using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    public class MobMissAnim : MonoBehaviour
    {
        public Color toColor = new Color(0.7f, 0.7f, 0.7f, 1f);
        private Action m_onComplete;
        public float timeIn = 0.3f;
        public float timeOut = 0.5f;


        void Start()
        {

        }

        public void Run(Action callback)
        {
            MobUI mobUI = GetComponentInChildren<MobUI>();
            if (mobUI == null)
                Debug.LogWarning("MobUI not found.");
            else
                mobUI.ShowMissText();
            m_onComplete = callback;
            var setup = LeanTween.color(gameObject, toColor, timeIn);
            setup.setOnComplete(PartOut);
        }

        private void PartOut()
        {
            var setup = LeanTween.color(gameObject, Color.white, timeOut);
            setup.setOnComplete(m_onComplete);
        }
    }

}
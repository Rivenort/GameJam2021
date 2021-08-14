using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class MobHitAnim : MonoBehaviour
    {
        public Color toColor;
        private Action m_onComplete;
        public float timeIn;
        public float timeOut;


        void Start()
        {

        }

        public void Run(Action callback)
        {
            Debug.Log("RUN");
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
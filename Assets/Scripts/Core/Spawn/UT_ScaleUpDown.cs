using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UT_ScaleUpDown : MonoBehaviour
    {

        public Vector3 scaleTo;
        public float scaleTime;
        private Vector3 m_originScale;
        private bool m_interrupted = false;

        void Start()
        {
            
        }

        public void Run()
        {
            m_interrupted = false;
            m_originScale = gameObject.transform.localScale;
            var setup = LeanTween.scale(gameObject, scaleTo, scaleTime);
            setup.setOnComplete(OnPartOne);
        }

        public void Interrupt()
        {
            m_interrupted = true;
        }

        void OnPartOne()
        {
            var setup = LeanTween.scale(gameObject, m_originScale, scaleTime);
            setup.setOnComplete(OnPartTwo);
        }

        void OnPartTwo()
        {
            if (m_interrupted)
                return;
            var setup = LeanTween.scale(gameObject, scaleTo, scaleTime);
            setup.setOnComplete(OnPartOne);
        }

    }

}
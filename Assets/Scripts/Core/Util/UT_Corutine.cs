using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BestGameEver
{

    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UT_Coroutine
    {
        private Coroutine m_coroutine;
        private object m_result;
        private IEnumerator m_target;

        public UT_Coroutine(MonoBehaviour owner, IEnumerator target)
        {
            m_target = target;
            m_coroutine = owner.StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            while (m_target.MoveNext())
            {
                m_result = m_target.Current;
                yield return m_result;
            }
        }

        public object GetResult()
        {
            return m_result;
        }
    }
}
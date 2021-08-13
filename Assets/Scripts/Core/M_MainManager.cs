using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// [Component Singleton]
    /// @author Rivenort
    /// </summary>
    public class M_MainManager : MonoBehaviour
    {
        private static M_MainManager s_instance = null;

        private List<UT_IDoOnGameStart> m_singOnGameStart = new List<UT_IDoOnGameStart>();
        private List<UT_IClearable> m_singClearable = new List<UT_IClearable>();
        private List<UT_IUpdateable> m_singUpdateable = new List<UT_IUpdateable>();


        void Awake()
        {

            if (s_instance != null && s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;

            ScanSingletons();
        }

        private void ScanSingletons()
        {
            Debug.Log("Scanning Singletons ... ");
            m_singOnGameStart.Clear();
            m_singClearable.Clear();
            m_singUpdateable.Clear();


            List<Type> typesDoOnGameStart = UT_Algorithms.GetSingletonsOf("Assembly-CSharp", typeof(UT_IDoOnGameStart));
            foreach (Type singleton in typesDoOnGameStart)
            {
                object obj = singleton.GetMethod("GetInstance").Invoke(null, null);
                m_singOnGameStart.Add((UT_IDoOnGameStart)obj);
            }

            List<Type> typesClearable = UT_Algorithms.GetSingletonsOf("Assembly-CSharp", typeof(UT_IClearable));
            foreach (Type singleton in typesClearable)
            {
                object obj = singleton.GetMethod("GetInstance").Invoke(null, null);
                m_singClearable.Add((UT_IClearable)obj);
            }

            List<Type> typesUpdateable = UT_Algorithms.GetSingletonsOf("Assembly-CSharp", typeof(UT_IUpdateable));
            foreach (Type singleton in typesUpdateable)
            {
                object obj = singleton.GetMethod("GetInstance").Invoke(null, null);
                m_singUpdateable.Add((UT_IUpdateable)obj);
            }
        }
   

    }

}
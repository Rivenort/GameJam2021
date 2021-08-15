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
        private List<UT_IOnMobActionCompleted> m_singOnMobAction = new List<UT_IOnMobActionCompleted>();
        private List<UT_IOnMobCreated> m_singOnMobCreated = new List<UT_IOnMobCreated>();
        private List<UT_IOnMobDestroyed> m_singOnMobDestroyed = new List<UT_IOnMobDestroyed>();
        private List<UT_IOnAllMobsDestroyed> m_singAllMobsDestroyed = new List<UT_IOnAllMobsDestroyed>();
        

        private PlayerType m_currentTurn = PlayerType.PLAYER_ONE;


        void Awake()
        {

            if (s_instance != null && s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;

            ScanSingletons();

            foreach (UT_IDoOnGameStart onGameStart in m_singOnGameStart)
            {
                onGameStart.OnGameStart();
            }
        }

        private void ScanSingletons()
        {
            Debug.Log("Scanning Singletons ... ");
            m_singOnGameStart.Clear();
            m_singClearable.Clear();
            m_singUpdateable.Clear();
            m_singOnMobAction.Clear();
            m_singOnMobCreated.Clear();
            m_singOnMobDestroyed.Clear();


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

            List<Type> typesOnMobAction = UT_Algorithms.GetSingletonsOf("Assembly-CSharp", typeof(UT_IOnMobActionCompleted));
            foreach (Type singleton in typesOnMobAction)
            {
                object obj = singleton.GetMethod("GetInstance").Invoke(null, null);
                m_singOnMobAction.Add((UT_IOnMobActionCompleted)obj);
            }

            List<Type> typesOnMobCreated = UT_Algorithms.GetSingletonsOf("Assembly-CSharp", typeof(UT_IOnMobCreated));
            foreach (Type singleton in typesOnMobCreated)
            {
                object obj = singleton.GetMethod("GetInstance").Invoke(null, null);
                m_singOnMobCreated.Add((UT_IOnMobCreated)obj);
            }

            List<Type> typesOnMobDestroyed = UT_Algorithms.GetSingletonsOf("Assembly-CSharp", typeof(UT_IOnMobDestroyed));
            foreach (Type singleton in typesOnMobDestroyed)
            {
                object obj = singleton.GetMethod("GetInstance").Invoke(null, null);
                m_singOnMobDestroyed.Add((UT_IOnMobDestroyed)obj);
            }

            List<Type> typesOnAllMobsDestroyed = UT_Algorithms.GetSingletonsOf("Assembly-CSharp", typeof(UT_IOnAllMobsDestroyed));
            foreach (Type singleton in typesOnAllMobsDestroyed)
            {
                object obj = singleton.GetMethod("GetInstance").Invoke(null, null);
                m_singAllMobsDestroyed.Add((UT_IOnAllMobsDestroyed)obj);
            }
        }

        private void Update()
        {
            foreach (UT_IUpdateable updateable in m_singUpdateable)
            {
                updateable.Update();
            }
        }

        private void Clear()
        {

            foreach (UT_IClearable clearable in m_singClearable)
            {
                clearable.Clear();
            }
        }

        private void CallOnMobActionCompleted(IMob mob)
        {
            foreach (UT_IOnMobActionCompleted singleton in m_singOnMobAction)
            {
                singleton.OnMobActionCompleted(mob);
            }
        }

        public static void SCallOnMobActionCompleted(IMob mob)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.CallOnMobActionCompleted(mob);
        }

        private void CallOnMobCreated(IMob mob)
        {
            foreach (var singleton in m_singOnMobCreated)
            {
                singleton.OnMobCreated(mob);
            }
        }

        private void CallOnAllMobsDestroyed(PlayerType playerWithNoMobs)
        {
            foreach (var singleton in m_singAllMobsDestroyed)
            {
                singleton.OnAllMobsDestroyed(playerWithNoMobs);
            }
        }

        public static void SCallOnAllMobsDestroyed(PlayerType playerWithNoMobs)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.CallOnAllMobsDestroyed(playerWithNoMobs);
        }

        public static void SCallOnMobCreated(IMob mob)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.CallOnMobCreated(mob);
        }

        private void CallOnMobDestroyed(Guid mobId)
        {
            foreach (var singleton in m_singOnMobDestroyed)
            {
                singleton.OnMobDestroyed(mobId);
            }
        }

        public static void SCallOnMobDestroyed(Guid mobId)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.CallOnMobDestroyed(mobId);
        }

    }

}
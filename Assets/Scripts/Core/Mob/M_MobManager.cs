using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{

    public delegate void OnAllPlayerMobsDestroyed(PlayerType player);
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_MobManager : UT_IDoOnGameStart, UT_IClearable, UT_IOnMobCreated, UT_IOnMobDestroyed
    {
        private static M_MobManager s_instance = null;
        private static readonly object s_lock = new object();

        private const string OBJECTNAME_MOBS = "Mobs";
        private Transform m_groupMobs = null;

        private Dictionary<Guid, IMob> m_mobs;
        private IMob m_choosedMob;

        private event OnAllPlayerMobsDestroyed m_eventAllPlayerMobsDestroyed;

        private M_MobManager()
        {
            m_mobs = new Dictionary<Guid, IMob>();
            m_groupMobs = GameObject.Find(OBJECTNAME_MOBS).transform;
        }

        public static M_MobManager GetInstance()
        {
            lock (s_lock)
            {
                if (s_instance == null)
                    s_instance = new M_MobManager();
                return s_instance;
            }
        }

        private void ScanMobs()
        {

            foreach (Transform child in m_groupMobs)
            {
                IMob mobComp = child.gameObject.GetComponent<IMob>();
                if (mobComp == null)
                    continue;
                m_mobs.Add(mobComp.GetId(), mobComp);
            }
        }

        public static void SAddListener_OnAllPlayerMobsDestroyed(OnAllPlayerMobsDestroyed func)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_eventAllPlayerMobsDestroyed += func;
        }

        public void OnGameStart()
        {
            ScanMobs();
        }


        private IMob GetMob(Guid id)
        {
            if (m_mobs.ContainsKey(id))
                return m_mobs[id];
            return null;
        }

        public static IMob SGetMob(Guid id)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.GetMob(id);
        }

        private void DealDamage(IMob attacker, IMob defender)
        {
            int damageDealt = defender.DealDamage(attacker.ComputeDamage());
            Debug.Log("DealDamage() -> Attacker: " + attacker.GetName() + " Defender: " + defender.GetName() + " DMG: " + damageDealt);
        }

        public static void SDealDamage(IMob attacker, IMob defender)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.DealDamage(attacker, defender);
        }

        public static Transform SGetMobGroup()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.m_groupMobs;
        }

        public void Clear()
        {
            m_mobs.Clear();
        }

        private void SetChoosenMob(IMob mob)
        {
            if (m_choosedMob != null && !m_choosedMob.GetId().Equals(mob.GetId()))
                m_choosedMob.CloseUI();
            m_choosedMob = mob;
        }

        public static void SSetChoosenMob(IMob mob) 
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.SetChoosenMob(mob);
        }

        public void OnMobCreated(IMob mob)
        {
            m_mobs.Add(mob.GetId(), mob);
        }

        private void HideUI()
        {
            foreach (var mob in m_mobs)
            {
                mob.Value.CloseUI();
            }
        }

        public static void SHideUI()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.HideUI();
        }

        private void SetDisableUI(bool val)
        {
            foreach (var mob in m_mobs)
            {
                if (val)
                    mob.Value.DisableUI();
                else
                    mob.Value.EnableUI();
            }
        }


        public static void SDisableUI()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.SetDisableUI(true);
        }

        public static void SEnableUI()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.SetDisableUI(false);
        }

        public void OnMobDestroyed(Guid mobId)
        {
            if (m_mobs.ContainsKey(mobId))
            {
                m_mobs.Remove(mobId);
            }

            int player1Mobs = 0;
            int player2Mobs = 0;
            // check if players has mobs
            foreach (var mob in m_mobs)
            {
                if (mob.Value.GetPlayer() == PlayerType.PLAYER_ONE)
                    player1Mobs++;
                if (mob.Value.GetPlayer() == PlayerType.PLAYER_TWO)
                    player2Mobs++;
            }

            if (player1Mobs == 0)
            {
                M_MainManager.SCallOnAllMobsDestroyed(PlayerType.PLAYER_ONE);
                m_eventAllPlayerMobsDestroyed?.Invoke(PlayerType.PLAYER_ONE);
            }
            else if (player2Mobs == 0)
            {
                M_MainManager.SCallOnAllMobsDestroyed(PlayerType.PLAYER_TWO);
                m_eventAllPlayerMobsDestroyed?.Invoke(PlayerType.PLAYER_TWO);
            }
        }
    }

}
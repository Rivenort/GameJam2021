using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_MobManager : UT_IDoOnGameStart
    {
        private static M_MobManager s_instance = null;
        private static readonly object s_lock = new object();

        private const string OBJECTNAME_MOBS = "Mobs";
        private Transform m_groupMobs = null;

        private Dictionary<Guid, IMob> m_mobs;

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
    }

}
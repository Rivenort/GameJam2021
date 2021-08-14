
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_SpawnManager : UT_IDoOnGameStart, UT_IClearable
    {
        private static M_SpawnManager s_instance = null;
        private static readonly object s_lock = new object();

        private const string OBJECTNAME_SPAWNS = "Spawns";
        private Transform m_groupSpawns = null;

        private List<SpawnPoint> m_spawnPoints = new List<SpawnPoint>();

        private M_SpawnManager()
        {
            m_groupSpawns = GameObject.Find(OBJECTNAME_SPAWNS).transform;
        }

        public static M_SpawnManager GetInstance()
        {
            lock (s_lock)
            {
                if (s_instance == null)
                    s_instance = new M_SpawnManager();
                return s_instance;
            }
        }

        public void OnGameStart()
        {
            foreach (Transform child in m_groupSpawns)
            {
                SpawnPoint spawn = child.gameObject.GetComponent<SpawnPoint>();
                if (spawn != null)
                    m_spawnPoints.Add(spawn);
            }
        }


        private void ExposeSpawnPoints(PlayerType playerType)
        {
            foreach (SpawnPoint spawnPoint in m_spawnPoints)
            {
                if (spawnPoint.GetPlayerType() == playerType)
                    spawnPoint.Expose();
            }
        }

        private void InterruptExposedSpawns()
        {
            foreach (SpawnPoint spawnPoint in m_spawnPoints)
            {
                spawnPoint.DisableExspose();
            }
        }

        public static void SExposeSpawnPoints(PlayerType playerType)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.ExposeSpawnPoints(playerType);
        }

        public static void SInterruprtExposedSpawns()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.InterruptExposedSpawns();
        }

        public void Clear()
        {
            m_groupSpawns.Clear();
        }
    }

}
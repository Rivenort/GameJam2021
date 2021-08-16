
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private Dictionary<Guid, SpawnPoint> m_spawnPoints = new Dictionary<Guid, SpawnPoint>();

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
                    m_spawnPoints.Add(spawn.GetId(), spawn);
            }
        }

        public static SpawnPoint SGetSpawn(Guid id)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            if (s_instance.m_spawnPoints.ContainsKey(id))
                return s_instance.m_spawnPoints[id];
            Debug.LogWarning("No Spawn point with id: " + id);
            return null;
        }

        private void ExposeSpawnPoints(PlayerType playerType)
        {
            M_MobManager.SDisableUI();
            foreach (var spawnPoint in m_spawnPoints)
            {
                if (spawnPoint.Value.GetPlayerType() == playerType &&
                    M_MapManager.SIsSpawnFree(spawnPoint.Value.GetPosition()))
                    spawnPoint.Value.Expose();
            }
        }


        public static List<SpawnPoint> SGetSpawns()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            List<SpawnPoint> res = new List<SpawnPoint>();
            foreach (var spawn in s_instance.m_spawnPoints)
            {
                res.Add(spawn.Value);
            }
            return res;
        }

        

        private void InterruptExposedSpawns()
        {
            M_MobManager.SEnableUI();
            foreach (var spawnPoint in m_spawnPoints)
            {
                spawnPoint.Value.DisableExspose();
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
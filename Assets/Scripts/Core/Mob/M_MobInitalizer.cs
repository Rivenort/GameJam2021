using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_MobInitalizer : MonoBehaviour
    {
        private static M_MobInitalizer s_instance = null;

        [SerializeField]
        private List<GameObject> m_mobs;

        private Dictionary<string, GameObject> m_mobsDict;
        [SerializeField]
        private string m_defaultPrefabP1;
        [SerializeField]
        private string m_defaultPrefabP2;

        private void Start()
        {

            if (s_instance != null & s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;
            ProcessPrefabs();
        }

        private void ProcessPrefabs()
        {
            m_mobsDict = new Dictionary<string, GameObject>();
            foreach (GameObject prefab in m_mobs)
            {
                m_mobsDict.Add(prefab.name, prefab);
            }
        }

        private GameObject Init(string name, SpawnPoint spawn)
        {
            if (!m_mobsDict.ContainsKey(name))
            {
                Debug.LogWarning("No registered prefab with the name: " + name);
                return null;
            }

            GameObject obj = GameObject.Instantiate(m_mobsDict[name]);
            IMob mob = obj.GetComponent<IMob>();
            obj.transform.SetParent(M_MobManager.SGetMobGroup());


            obj.transform.position = spawn.transform.position;

            M_MainManager.SCallOnMobCreated(mob);

            return obj;
        }

        public static GameObject SInit(string name, SpawnPoint spawn)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.Init(name, spawn);
        }

        public static GameObject SInitP1(SpawnPoint spawn)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.Init(s_instance.m_defaultPrefabP1, spawn);
        }

        public static GameObject SInitP2(SpawnPoint spawn)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.Init(s_instance.m_defaultPrefabP2, spawn);
        }

        public static void SSetP1Default(string prefabName)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_defaultPrefabP1 = prefabName;
        }

        public static void SSetP2Default(string prefabName)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_defaultPrefabP2 = prefabName;
        }
    }

}
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
        private string m_defaultPrefab;

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

            float yDiff = spawn.transform.position.y - mob.GetRootLocalPos().y;
            Debug.Log("DIFF: " + yDiff);
            Vector3 newPos = spawn.transform.position;
            newPos.y = newPos.y + yDiff;

            obj.transform.position = newPos;

            M_MainManager.SCallOnMobCreated(mob);

            return obj;
        }

        public static GameObject SInit(string name, SpawnPoint spawn)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.Init(name, spawn);
        }

        public static GameObject SInit(SpawnPoint spawn)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.Init(s_instance.m_defaultPrefab, spawn);
        }

    }

}
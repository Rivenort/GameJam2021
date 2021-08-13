using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

namespace BestGameEver
{
    /// <summary>
    /// [Singleton]
    /// @author Rivenort
    /// </summary>
    public class M_LoadingManager
    {
        private static M_LoadingManager s_instance = null;
        private static readonly object s_lock = new object();

        private UT_LoadingHelper m_loadingHelper;
        private DAT_SOGameConfiguration m_config;
        private const string ADDR_CONFIG = "GameConfig";

        private M_LoadingManager()
        {
            m_loadingHelper = new UT_LoadingHelper();
            LoadConfig();
        }

        public static M_LoadingManager GetInstance()
        {
            lock (s_lock)
            {
                if (s_instance == null)
                    s_instance = new M_LoadingManager();
                return s_instance;
            }
        }


        private void LoadConfig()
        {
            var handle = Addressables.LoadAssetAsync<DAT_SOGameConfiguration>(ADDR_CONFIG);
            handle.Completed += (han) =>
            {
                if (han.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError("Couldn't load addressable: " + ADDR_CONFIG);
                    return;
                }

                m_config = han.Result;
            };
        }
        
        private IEnumerator Load()
        {

            while (m_config == null)
            {
                Debug.Log("Waiting for config to be loaded... ");
                yield return new LoadingProgress(0f, "Waiting for config to be loaded... ");
            }
            yield return new LoadingProgress(0.1f,  "Loading assets...");

            m_loadingHelper.LoadAssets(m_config);

            M_SceneLoaderManager.SLoadScene(2);
        }

        public static IEnumerator SLoad()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            Debug.Log("Load");
            return s_instance.Load();
        }

        public static List<SpriteAtlas> SGetLoadedAtlases()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.m_loadingHelper.GetLoadedSpriteAtlases();
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

namespace BestGameEver
{
    /// <summary>
    /// The following class is up to queue the loading of assets.
    /// It looks for Singletons that implement <see cref="UT_IRequiresLoading"/> interface and
    /// runs the Load() method.
    /// 
    /// Additionally it can save the references to the SpriteAtlas objects in order
    /// to further usage in e.g. <see cref="M_AtlasRegisterer"/>.
    /// You can disable this feature.
    /// 
    /// @author Rivenort
    /// </summary>
    public class UT_LoadingHelper
    {

        private int m_currentAssetsLoaded;
        private int m_assetsToLoad;
        private bool m_performed;

        private bool m_saveAtlases = true;
        private List<SpriteAtlas> m_spriteAtlases;

        public UT_LoadingHelper()
        {
            m_spriteAtlases = new List<SpriteAtlas>();
        }

        public void LoadAssets(DAT_SOGameConfiguration config)
        {
            m_performed = false;
            m_currentAssetsLoaded = 0;
            m_assetsToLoad = 0;
            Debug.Log("Scanning...");
            m_currentAssetsLoaded = 0;
            List<Type> toLoadClasses = UT_Algorithms.GetSingletons("Assembly-CSharp");

            foreach (Type type in toLoadClasses)
            {
                if (typeof(UT_IRequiresLoading).IsAssignableFrom(type))
                {
                    MethodInfo getInstance = type.GetMethod("GetInstance");
                    if (getInstance == null)
                        continue;

                    UT_IRequiresLoading obj = (UT_IRequiresLoading)getInstance.Invoke(null, null);
                    if (obj != null)
                    {
                        m_assetsToLoad += obj.Load(AssetLoadedCallback, config);
                    }
                }
            }
            Debug.Log("AssetsToLoad: " + m_assetsToLoad);
        }

        private void AssetLoadedCallback(AsyncOperationHandle handle)
        {
            if (handle.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError("Coundn't Load asset: " + handle.DebugName);
                return;
            }

            Debug.Log("Asset Loaded: " + handle.Result.ToString());
            if (m_saveAtlases && handle.Result.GetType() == typeof(SpriteAtlas))
            {
                m_spriteAtlases.Add((SpriteAtlas)handle.Result);
            }
            m_currentAssetsLoaded++;
            if (m_currentAssetsLoaded >= m_assetsToLoad)
            {
                Debug.Log("All assets loaded: " + m_currentAssetsLoaded);
                m_performed = true;
            }
        }

        public bool IsLoaded()
        {
            return m_performed;
        }

        public string GetProgressMessage()
        {
            return string.Format("{0} out of {1}", m_currentAssetsLoaded, m_assetsToLoad);
        }

        public float GetProgress()
        {
            return m_currentAssetsLoaded * 1f / m_assetsToLoad;
        }

        public List<SpriteAtlas> GetLoadedSpriteAtlases()
        {
            return m_spriteAtlases;
        }
    }

}
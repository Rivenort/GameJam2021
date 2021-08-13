using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace BestGameEver
{
    /// <summary>
    /// Loading Scene short api.
    /// @author Rivenort
    /// </summary>
    public class M_SceneLoaderManager : MonoBehaviour
    {
        private static M_SceneLoaderManager s_instance = null;

        private Action<float> m_onUpdate = null;

        private void Start()
        {
            if (s_instance != null && s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;
        }


        public void SetOnUpdate(Action<float> onUpdate)
        {
            m_onUpdate = onUpdate;
        }

        public static void SSetOnUpdate(Action<float> onUpdate)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.SetOnUpdate(onUpdate);
        }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadAsync(sceneIndex));
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadAsync(SceneManager.GetSceneByName(sceneName).buildIndex));
        }

        public static void SLoadScene(int sceneIndex)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.LoadScene(sceneIndex);
        }

        public static void SLoadScene(string sceneName)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.LoadScene(sceneName);
        }

        public static IEnumerator SLoadAsync(int sceneIndex)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            yield return s_instance.LoadAsync(sceneIndex);
        }

        IEnumerator LoadAsync(int sceneIndex)
        {
            Debug.Log("Loading (" + sceneIndex + ") scene...");

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);

                m_onUpdate?.Invoke(progress);

                yield return null;
            }

        }

    }
}

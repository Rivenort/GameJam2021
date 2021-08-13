using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace BestGameEver
{
    /// <summary>
    /// [Component Singleton]
    /// The purpose is to use the atlas for the first time.
    /// On the first use, Unity registers the given spriteAtlas (it takes some CPU time).
    /// It does use the trick to presetup the Unity's processings.
    /// @author Rivenort
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class M_AtlasRegisterer : MonoBehaviour
    {
        private static M_AtlasRegisterer s_instance = null;

        private Image m_image;
        private List<SpriteAtlas> m_atlases;
        private bool m_processing;
        private int m_current;
        private bool m_performed = false;


        void Start()
        {

            if (s_instance != null & s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;

            m_image = GetComponent<Image>();
            m_atlases = new List<SpriteAtlas>();

            AddAtlases(M_LoadingManager.SGetLoadedAtlases());
            Flush();
        }


        public void AddAtlases(List<SpriteAtlas> atlases)
        {
            m_atlases.AddRange(atlases);
            Debug.Log("Added Atlases: " + atlases.Count);
        }

        public static void SAddAtlases(List<SpriteAtlas> atlases)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.AddAtlases(atlases);
        }


        private void Flush()
        {
            Debug.Log("Flushed.");
            m_processing = true;
            m_current = 0;
            m_performed = false;
        }

        private void Clear()
        {
            Debug.Log("Cleared.");
            m_image.sprite = null;
            m_atlases.Clear();
            m_processing = false;
            m_current = 0;
            m_performed = true;
        }

        public static void SFlush()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.Flush();
        }

        void Update()
        {

            if (m_processing)
            {
                if (m_atlases.Count == 0)
                {
                    Clear();
                    return;
                }
                SpriteAtlas atlas = m_atlases[m_current];
                Sprite[] sprites = new Sprite[m_atlases[m_current].spriteCount];
                atlas.GetSprites(sprites);
                m_image.sprite = m_atlases[m_current].GetSprite(sprites[0].name.Replace("(Clone)", ""));
                m_current++;
                if (m_current >= m_atlases.Count)
                {
                    Clear();
                }
            }
        }

        public static bool SIsLoaded()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.m_performed;
        }
    }
}
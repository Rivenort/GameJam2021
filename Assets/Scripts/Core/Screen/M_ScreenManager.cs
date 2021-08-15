using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// [Component Singleton]
    /// Screen Manager manages the UI gameobjects called "Screen".
    /// @author Rivenort
    /// </summary>
    public class M_ScreenManager : MonoBehaviour
    {
        private static M_ScreenManager s_instance = null;

        [Space(10)]
        public GameObject[] screensToBeInitializedOnStart;
        private bool m_activated = false;


        private List<GameObject> m_previousScreens = new List<GameObject>();
        private GameObject m_currentScreen = null;
        public GameObject backButton;


        private void Start()
        {
            if (s_instance != null && s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;

            Debug.Log("ScreenManager -> Activating Screens");
            if (!m_activated)
            {
                foreach (GameObject o in screensToBeInitializedOnStart)
                {
                    Debug.Log(string.Format("Activating \"{0}\"", o.name));
                    o.SetActive(true);
                }
                m_activated = true;
            }

        }


        private void Update()
        {
            if (m_activated)
            {
                Debug.Log("ScreenManager -> Deactivating Screens");
                foreach (GameObject o in screensToBeInitializedOnStart)
                {
                    Debug.Log(string.Format("Deactivating \"{0}\"", o.name));
                    o.SetActive(false);
                }
                m_activated = false;
            }

        }


        public void ChangeScreenPersistentBack(GameObject screen)
        {

            if (m_currentScreen == screen)
            {
                PreviousScreen();
            }
            else
            {
                if (m_currentScreen != null)
                {

                    m_currentScreen.SetActive(false);
                    //M_EventsManager.SPublishEvent(new EventScreenDisabled(m_currentScreen));

                    if (m_previousScreens.Contains(m_currentScreen))
                        m_previousScreens.Remove(m_currentScreen);
                    if (m_previousScreens.Contains(screen))
                        m_previousScreens.Remove(screen);
                    m_previousScreens.Add(m_currentScreen);
                }
                m_currentScreen = screen;
                m_currentScreen.SetActive(true);
                //backButton.SetActive(true);

                //M_EventsManager.SPublishEvent(new EventBlockCamera());
                //M_EventsManager.SPublishEvent(new EventScreenEnabled(m_currentScreen));
            }
            if (m_currentScreen != null)
            {
                /*
                M_PauseManager.SFreezeGame(GetHashCode());
                if (SystemInfo.deviceType == DeviceType.Handheld)
                    M_GameHelper.SSetCameraMovement(false); */
            }

        }


        public void PreviousScreen()
        {

            if (m_currentScreen != null)
            {
                m_currentScreen.SetActive(false);
                //M_EventsManager.SPublishEvent(new EventUnblockCamera());
                //M_EventsManager.SPublishEvent(new EventScreenDisabled(m_currentScreen));
            }
            if (m_previousScreens.Count > 0)
            {
                m_currentScreen = m_previousScreens[m_previousScreens.Count - 1];
                m_previousScreens.RemoveAt(m_previousScreens.Count - 1);
                m_currentScreen.SetActive(true);
                //M_EventsManager.SPublishEvent(new EventBlockCamera());
                //M_EventsManager.SPublishEvent(new EventScreenEnabled(m_currentScreen));
            }
            else
            {
                m_currentScreen = null;
                //backButton.SetActive(false);
            }
            if (m_currentScreen == null)
            {
                /*
                M_PauseManager.SResumeFreezedGame(GetHashCode());
                if (SystemInfo.deviceType == DeviceType.Handheld)
                    M_GameHelper.SSetCameraMovement(true); */
            }

        }


        public static void SHideScreen()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.PreviousScreen();

        }



        public static void SChangeScreenPersistentBack(GameObject screen)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.ChangeScreenPersistentBack(screen);

        }

        public static void SPreviousScreen()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.PreviousScreen();

        }

        public void CloseAll()
        {
            while (m_currentScreen != null)
                PreviousScreen();
        }

        public static void SCloseAll()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.CloseAll();
        }
    }
}
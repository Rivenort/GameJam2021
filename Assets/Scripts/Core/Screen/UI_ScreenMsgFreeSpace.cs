using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// [Component Singleton]
    /// @author Rivenort 
    /// </summary>
    public class UI_ScreenMsgFreeSpace : MonoBehaviour
    {
        private static UI_ScreenMsgFreeSpace s_instance = null;

        void Awake()
        {
            if (s_instance != null && s_instance != this)
                throw new CE_SingletonNotInitialized();
            s_instance = this;
        }


        public static void SShow()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.gameObject.SetActive(true);
        } 
    }

}
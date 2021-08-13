using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort 
    /// </summary>
    public class UI_ScreenLoading : MonoBehaviour
    {

        public TMP_Text uiTextLog;
        public Slider uiSlider;

        private UT_Coroutine m_loadingProgress = null;

        void Start()
        {
            if (uiTextLog == null ||
                uiSlider == null)
                throw new CE_ComponentNotFullyInitialized();

            M_LoadingManager.GetInstance();
            m_loadingProgress = new UT_Coroutine(this, M_LoadingManager.SLoad());
        }

        private void Update()
        {
            
            if (m_loadingProgress != null && m_loadingProgress.GetResult().GetType() == typeof(LoadingProgress))
            {
                LoadingProgress progress = (LoadingProgress) m_loadingProgress.GetResult();
                uiTextLog.text = progress.message;
                uiSlider.value = progress.progress;
            }
        }


    }

}
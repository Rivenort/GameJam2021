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
    public class UI_HealthBar : MonoBehaviour
    {
        public Slider uiSlider;
        public TMP_Text uiText;

        void Start()
        {
            if (uiSlider == null ||
                uiText == null)
                throw new CE_ComponentNotFullyInitialized();
        }

        public void SetValue(int current, int max)
        {
            uiText.text = current + "/" + max;
            uiSlider.value = 1f * current / max;
        }


    }

}
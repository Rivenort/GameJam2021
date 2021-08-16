using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// @author Rivenort
/// </summary>
namespace BestGameEver
{

    /// <summary>
    /// @author Rivenort
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class UI_MatchResolution : MonoBehaviour
    {
        public Vector3 targetScale = new Vector3(1f, 1f, 1f);
        

        void Start()
        {
            RectTransform rt = GetComponent<RectTransform>();
            rt.localScale = targetScale;
        }

    }

}
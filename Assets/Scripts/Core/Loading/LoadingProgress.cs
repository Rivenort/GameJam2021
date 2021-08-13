using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public struct LoadingProgress
    {
        public float progress;
        public string message;

        public LoadingProgress(float progress, string message)
        {
            this.progress = progress;
            this.message = message;
        }
    }
}
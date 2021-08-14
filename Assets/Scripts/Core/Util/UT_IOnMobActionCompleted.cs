using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// All Singletons that inherit the following interface
    /// will be called by this method.
    /// @author Rivenort
    /// </summary>
    public interface UT_IOnMobActionCompleted
    {
        void OnMobActionCompleted(IMob mob);
    }

}
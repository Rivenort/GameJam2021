using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{

    /// <summary>
    /// @author Rivenort
    /// </summary>
    public interface UT_IUpdateable
    {
        /// <summary>
        /// If a Singleton implements the interface, the following method is gonna be 
        /// automatically called per each update.
        /// </summary>
        void Update();
    }

}
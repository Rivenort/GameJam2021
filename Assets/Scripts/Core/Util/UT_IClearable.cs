using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public interface UT_IClearable
    {
        /// <summary>
        /// If the singleton implements the interface, the following method is gonna
        /// be called on game scene exit.
        /// </summary>
        void Clear();
    }

}
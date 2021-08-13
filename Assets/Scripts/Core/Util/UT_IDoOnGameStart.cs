using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{

    /// <summary>
    /// All singletons with this interface are gonna be called.
    /// @author Rivenort
    /// </summary>
    public interface UT_IDoOnGameStart
    {
        void OnGameStart();
    }

}
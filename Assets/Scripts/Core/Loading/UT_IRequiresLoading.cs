using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace BestGameEver
{
    /// <summary>
    /// The Singleton class should implement this if it requires any
    /// asset loading .
    /// @author Rivenort
    /// </summary>
    public interface UT_IRequiresLoading
    {
        /// <summary>
        /// Setups Addressables Loading.
        /// </summary>
        /// <param name="onComplete">callback that has to be called on loading completed</param>
        /// <param name="config">game config</param>
        /// <returns>Number of assets being loaded</returns>
        int Load(Action<AsyncOperationHandle> onComplete, DAT_SOGameConfiguration config);
    }

}
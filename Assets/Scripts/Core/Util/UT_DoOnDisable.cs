using UnityEngine;
using UnityEngine.Events;

namespace Error300.Util
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UT_DoOnDisable : MonoBehaviour
    {
        public UnityEvent onDisable;

        private void OnDisable()
        {
            onDisable?.Invoke();
        }

    }

}

using UnityEngine;
using UnityEngine.Events;

namespace Error300.Util
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UT_DoOnEnable : MonoBehaviour
    {
        public UnityEvent onEnable;

        private void OnEnable()
        {
            onEnable?.Invoke();
        }
    }
}
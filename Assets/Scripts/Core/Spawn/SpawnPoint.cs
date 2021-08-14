using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        private ClickableObject m_clickableObject;
        private UT_ScaleUpDown m_scaleAnim;

        void Start()
        {
            m_clickableObject = GetComponentInChildren<ClickableObject>();
            m_scaleAnim = GetComponent<UT_ScaleUpDown>();
            m_clickableObject.disabled = true;
            m_clickableObject.Func = OnClick;
        }

        private void OnClick()
        {
            M_MobInitalizer.SInit(this);
            M_SpawnManager.SInterruprtExposedSpawns();
        }

        public void Expose()
        {
            m_clickableObject.disabled = false;
            m_scaleAnim.Run();
        }

        public void DisableExspose()
        {
            m_clickableObject.disabled = true;
            m_scaleAnim.Interrupt();
        }
    }

}
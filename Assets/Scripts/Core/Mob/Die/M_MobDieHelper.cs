using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_MobDieHelper : MonoBehaviour
    {
        private static M_MobDieHelper s_instance = null;

        public GameObject poofAnim;

        void Start()
        {
            if (s_instance != null & s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;

        }

        private void PlayAnimOn(Vector3 position)
        {
            poofAnim.transform.position = position;
            poofAnim.SetActive(true);
        }

        public static void SPlayAnimOn(Vector3 position)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.PlayAnimOn(position);
        }

        private void OnAnimFinished()
        {
            poofAnim.gameObject.SetActive(false);
        }

        public static void SOnAnimFinished()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.OnAnimFinished();
        }

    }

}
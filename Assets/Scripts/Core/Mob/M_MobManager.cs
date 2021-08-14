using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_MobManager
    {
        private static M_MobManager s_instance = null;
        private static readonly object s_lock = new object();

        private M_MobManager()
        {

        }

        public static M_MobManager GetInstance()
        {
            lock (s_lock)
            {
                if (s_instance == null)
                    s_instance = new M_MobManager();
                return s_instance;
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    [RequireComponent(typeof(IMob))]
    public class MobStatsSetup : MonoBehaviour
    {
        public CardTemplate data;

        private void OnEnable()
        {
            IMob mob = GetComponent<IMob>();
            mob.SetStats(data);
        }


    }

}
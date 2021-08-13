using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class ATK_Melee : MonoBehaviour, IAttackAction
    {

        void Start()
        {

        }


        void Update()
        {

        }

        public void Perform(Action callback)
        {

        }

        AttackType IAttackAction.GetType()
        {
            return AttackType.MELEE;
        }
    }

}
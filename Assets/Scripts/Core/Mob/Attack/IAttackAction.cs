using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public interface IAttackAction
    {
        void Perform(Action callback);

        AttackType GetType();
    }
}
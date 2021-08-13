using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public interface IMob
    {
        Guid GetId();

        string GetName();

        void PerformAttack(Action callback);

        void PerformAbility(Action callback);

        void PerformGoLeft(Action callback);

        void PerformGoRight(Action callback);

        void PerformGoUp(Action callback);

        void PerformGoDown(Action callback);

        MobStats GetStats();


    }
}
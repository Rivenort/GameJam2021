using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public interface IAbility
    {
        int GetId();

        string GetName();

        void Perform(Action callback);
    }

}
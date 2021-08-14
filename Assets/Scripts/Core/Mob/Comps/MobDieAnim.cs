using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class MobDieAnim : MonoBehaviour
    {

        public float duration;

        void Start()
        {
            
        }

        public void Run(Action callback)
        {
            var setup = LeanTween.color(gameObject, Color.clear, duration);
            setup.setOnComplete(callback);
            M_MobDieHelper.SPlayAnimOn(gameObject.transform.position);
        }

        
    }

}
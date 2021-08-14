using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class MobMelee : MonoBehaviour, IMob
    {
        private Guid m_id;
        [SerializeField]
        private string m_name;
        [SerializeReference]
        private MobStats m_stats;

        private MobMotor m_motor;

        void Start()
        {
            if (m_id == Guid.Empty)
                m_id = Guid.NewGuid();
            m_motor = GetComponentInChildren<MobMotor>();
            if (m_motor == null)
                throw new CE_ExpectedElementNotFound("Mob's parent object does not have " + typeof(MobMotor).Name + " element.");
        }

        public Guid GetId()
        {
            return m_id;
        }

        public string GetName()
        {
            return m_name;
        }

        public MobStats GetStats()
        {
            return m_stats;
        }

        public void SetStats(MobStats stats)
        {
            m_stats = stats;
        }

        public void PerformAbility(Action callback)
        {

        }

        public void PerformAttack(Action callback)
        {

        }

        public void PerformGoDown(Action callback)
        {
            m_motor.MoveDown(callback);
        }

        public void PerformGoLeft(Action callback)
        {
            m_motor.MoveLeft(callback);
        }

        public void PerformGoRight(Action callback)
        {
            m_motor.MoveRight(callback);
        }

        public void PerformGoUp(Action callback)
        {
            m_motor.MoveUp(callback);
        }



    }

}
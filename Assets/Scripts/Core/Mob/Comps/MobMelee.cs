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
        PlayerType m_player;
        [SerializeField]
        private string m_name;
        [SerializeReference]
        private MobStats m_stats;

        private MobMotor m_motor;

        void Start()
        {
            if (m_id == Guid.Empty)
            {
                m_id = Guid.NewGuid();
            }
            m_motor = GetComponentInChildren<MobMotor>();
            if (m_motor == null)
                throw new CE_ExpectedElementNotFound("Mob's parent object does not have " + typeof(MobMotor).Name + " element.");
        }

        public Guid GetId()
        {
            if (m_id == Guid.Empty)
                m_id = Guid.NewGuid();
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
            IMob enemy = M_MapManager.SGetMeleeEnemy(m_player, GetRootPosition());
            if (enemy != null)
                M_MobManager.SDealDamage(this, enemy);
            callback?.Invoke();
        }

        public void PerformGoDown(Action callback)
        {
            m_motor.MoveDown(callback);
        }

        public void PerformGoRight(Action callback)
        {
            m_motor.MoveRight(callback);
        }

        public void PerformGoLeft(Action callback)
        {
            m_motor.MoveLeft(callback);
        }

        public void PerformGoUp(Action callback)
        {
            m_motor.MoveUp(callback);
        }

        public Vector3 GetRootPosition()
        {
            if (m_motor == null)
                m_motor = GetComponentInChildren<MobMotor>();
            return m_motor.gameObject.transform.position;
        }

        public PlayerType GetPlayer()
        {
            return m_player;
        }

        public int DealDamage(int damage)
        {
            int rand = UnityEngine.Random.Range(0, 100);
            if (rand < m_stats.GetArmour())
                return 0;

            m_stats.DealDamage(damage);
            return damage;
        }

        public int ComputeDamage()
        {
            int rand = UnityEngine.Random.Range(0, 100);
            if (rand < m_stats.GetHitChance())
                return m_stats.GetAttack();
            return 0;
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class Mob : MonoBehaviour, IMob
    {
        private Guid m_id;
        [SerializeField]
        PlayerType m_player;
        [SerializeReference]
        private MobStats m_stats;

        private MobMotor m_motor;
        private Animator m_animator;

        void Start()
        {
            if (m_id == Guid.Empty)
            {
                m_id = Guid.NewGuid();
            }
            m_motor = GetComponentInChildren<MobMotor>();
            if (m_motor == null)
                throw new CE_ExpectedElementNotFound("Mob's parent object does not have " + typeof(MobMotor).Name + " element.");
            m_animator = GetComponentInChildren<Animator>();
            if (m_animator == null)
                throw new CE_RequiredObjectNotInitialized("Couldnt find an animator in children of: " + gameObject.name);
        }

        public Guid GetId()
        {
            if (m_id == Guid.Empty)
                m_id = Guid.NewGuid();
            return m_id;
        }

        public string GetName()
        {
            return gameObject.name;
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
            IMob enemy = null;
            if (m_stats.GetAttackType() == AttackType.MELEE)
                enemy = M_MapManager.SGetEnemyForMelee(m_player, GetRootPosition());
            else if (m_stats.GetAttackType() == AttackType.RANGE)
                enemy = M_MapManager.SGetEnemyForRanger(m_player, GetRootPosition());

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
            MobHitAnim hitAnim = GetComponent<MobHitAnim>();
            if (hitAnim != null)
                hitAnim.Run(() => { });

            if (m_stats.GetHp() == 0)
            {
                var compDie = GetComponent<MobDieAnim>();


                M_MobDieHelper.SPlayAnimOn(gameObject.transform.position);
                compDie.Run(OnDieCallback);
            }

            return damage;
        }

        private void OnDieCallback()
        {
            GameObject.Destroy(gameObject);
            M_MainManager.SCallOnMobDestroyed(m_id);
        }

        public int ComputeDamage()
        {
            int rand = UnityEngine.Random.Range(0, 100);
            if (rand < m_stats.GetHitChance())
                return m_stats.GetAttack();
            return 0;
        }

        public void PlayAnimWalk(bool val)
        {
            m_animator.SetBool("IsWalking", val);
        }

        public void PlayAnimAttack()
        {
            m_animator.SetTrigger("Shoot");
        }

        public Vector3 GetRootLocalPos()
        {
            if (m_motor == null)
                m_motor = GetComponentInChildren<MobMotor>();
            return m_motor.transform.localPosition;
        }

        public void CloseUI()
        {
            GetComponentInChildren<MobUI>().ClosePanel();
        }
    }

}
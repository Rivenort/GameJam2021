using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    [Serializable]
    public class MobStats
    {
        [SerializeField]
        private int m_hp;

        [SerializeField]
        private int m_maxHp;

        [SerializeField]
        private int m_attack;

        [SerializeField]
        private int m_costAttack;

        [SerializeField]
        private int m_costMove;

        [SerializeField]
        private float m_armour;

        [SerializeField]
        private AttackType m_attackType;

        [SerializeField]
        private float m_hitChance;

        [SerializeField]
        private int m_range;


        public int GetHp()
        {
            return m_hp;
        }

        public int GetMaxHp()
        {
            return m_maxHp;
        }

        public int GetAttack()
        {
            return m_attack;
        }

        public int GetCostMove()
        {
            return m_costMove;
        }

        public int GetCostAttack()
        {
            return m_costAttack;
        }

        public int GetRange()
        {
            return m_range;
        }

        public float GetArmour()
        {
            return m_armour;
        }

        public AttackType GetAttackType()
        {
            return m_attackType;
        }

        public float GetHitChance()
        {
            return m_hitChance;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(typeof(MobStats).Name).Append(":{");
            stringBuilder.Append("\nm_hp: ").Append(m_hp);
            stringBuilder.Append("\nm_maxHp: ").Append(m_maxHp);
            stringBuilder.Append("\nm_attack: ").Append(m_attack);
            stringBuilder.Append("\nm_armour: ").Append(m_armour);
            stringBuilder.Append("\nm_costMove: ").Append(m_costMove);
            stringBuilder.Append("\nm_costAttack: ").Append(m_costAttack);
            stringBuilder.Append("\nm_attackType: ").Append(m_attackType);
            stringBuilder.Append("\nm_hitChance: ").Append(m_hitChance);
            stringBuilder.Append("\nm_range: ").Append(m_range).Append("\n}");

            return stringBuilder.ToString();
        }
    }

}
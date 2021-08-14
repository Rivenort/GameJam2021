using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BestGameEver
{
    [CreateAssetMenu(menuName = "Card", order = 1)]

    public class CardTemplate : ScriptableObject
    {
        public Sprite Character;
        public string Name;
        public int Cost;
        public int Distance;
        public int MoveCost;
        public int AttackType;
        public int Attack;
        public int AttackCost;
        public int AttackChance;
        public int Hp;
        public int Armour;
    }
}
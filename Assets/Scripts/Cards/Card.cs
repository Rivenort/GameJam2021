using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI T_Name, T_Cost, T_Distance, T_Attack, T_Deffence, T_Hp;

    [SerializeField] GameObject _sprite;
    string Name;
    int Cost, MoveCost, AttackType, Distance, Attack, AttackCost, Hp, Armour;

    public void SetData(Sprite sprite,string name,  int cost, int moveCost,int attackType,int distance, int attack,int attackCost, int hp,int armour)
    {
        Name = name;
        Cost = cost;
        MoveCost = moveCost;
        AttackType = attackType;
        Distance = distance;
        Attack = attack;
        AttackCost = attackCost;
        Hp = hp;
        Armour = armour;


        T_Name.text = name;
        T_Cost.text = cost.ToString();
        T_Distance.text = distance.ToString();
        T_Attack.text = attack.ToString();
        T_Deffence.text = armour.ToString();
        T_Hp.text = hp.ToString();

        _sprite.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
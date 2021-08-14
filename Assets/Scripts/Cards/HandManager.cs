using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{

    public class HandManager : MonoBehaviour
    {
        public CardsManager cardsManager;
        public CardTemplate[] Hand;
        public int CardCount = 0;

        Vector3 Position;

        [SerializeField]
        public GameObject MeleeCardPrefab;
        [SerializeField]
        public GameObject DistanceCardPrefab;
        [SerializeField]
        public GameObject CardSpawner;

        void Start()
        {
            AddNewCard();
            AddNewCard();
        }

        public void AddNewCard()
        {
            if (CardCount < 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (Hand[i] == null)
                    {
                        Hand[i] = cardsManager.DrawCard();
                        CardCount++;
                        break;
                    }
                }
            }
        }
        public void ShowCards()
        {
            Position = new Vector3 (0, -3.5f, 0);

            if (CardCount % 2 == 0)
            {
                Position.x = -0.05f;
            }
            else { Position.x = 0; }

            for(int i = 0; i < CardCount; i++)
            {
                if (Hand[i].AttackType == 0)
                {
                    GameObject instCard = Instantiate(MeleeCardPrefab, Position, Quaternion.identity, CardSpawner.transform);
                    instCard.GetComponent<Card>().SetData(Hand[i].Name, Hand[i].Cost,Hand[i].MoveCost,Hand[i].AttackType,Hand[i].Distance,Hand[i].Attack,Hand[i].AttackCost, Hand[i].Hp, Hand[i].Armour);



                    if (i % 2 == 0)
                    {
                        Position.x *= -1;
                        Position.x += 1.5f;
                    }
                    if (i % 2 == 1)
                    {
                        Position.x *= -1;
                    }
                }
                if (Hand[i].AttackType == 1)
                {
                    GameObject instCard = Instantiate(DistanceCardPrefab, Position, Quaternion.identity, CardSpawner.transform);
                    instCard.GetComponent<Card>().SetData(Hand[i].Name, Hand[i].Cost, Hand[i].MoveCost, Hand[i].AttackType, Hand[i].Distance, Hand[i].Attack, Hand[i].AttackCost, Hand[i].Hp, Hand[i].Armour);

                    if (i % 2 == 0)
                    {
                        Position.x *= -1;
                        Position.x += 1.5f;
                    }
                    if (i % 2 == 1)
                    {
                        Position.x *= -1;
                    }
                }
            }
        }
        public void HideCards()
        {
                for(int n=0;n<CardCount;n++)
                {
                    Destroy(CardSpawner.transform.GetChild(n).gameObject);   
            }
        }
    }
}
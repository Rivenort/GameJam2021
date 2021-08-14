using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{

    public class HandManager : MonoBehaviour
    {
        public CardsManager cardsManager;
        public CardTemplate[] Hand1;
        public CardTemplate[] Hand2;

        public int CardCount1 = 0;
        public int CardCount2 = 0;

        Vector3 Position;

        [SerializeField]
        public GameObject MeleeCardPrefab;
        [SerializeField]
        public GameObject DistanceCardPrefab;
        [SerializeField]
        public GameObject CardSpawner;

        void Start()
        {
            AddNewCard(1);
            AddNewCard(1);

            AddNewCard(2);
            AddNewCard(2);

        }

        public void AddNewCard(int j)
        {
            if (j == 1)
            {
                if (CardCount1 < 8)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (Hand1[i] == null)
                        {
                            Hand1[i] = cardsManager.DrawCard(1);
                            CardCount1++;
                            break;
                        }
                    }
                }
            }

            if (j == 2)
            {
                print("asd");
                if (CardCount2 < 8)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        if (Hand2[i] == null)
                        {
                            Hand2[i] = cardsManager.DrawCard(2);
                            CardCount2++;
                            break;
                        }
                    }
                }
            }
        }
        public void ShowCards(int j)
        {
            if (j == 1)
            {
                Position = new Vector3(0, -3.5f, 0);

                if (CardCount1 % 2 == 0)
                {
                    Position.x = -0.05f;
                }
                else { Position.x = 0; }

                for (int i = 0; i < CardCount1; i++)
                {
                    if (Hand1[i].AttackType == 0)
                    {
                        GameObject instCard = Instantiate(MeleeCardPrefab, Position, Quaternion.identity, CardSpawner.transform);
                        instCard.GetComponent<Card>().SetData(Hand1[i].Name, Hand1[i].Cost, Hand1[i].MoveCost, Hand1[i].AttackType, Hand1[i].Distance, Hand1[i].Attack, Hand1[i].AttackCost, Hand1[i].Hp, Hand1[i].Armour);



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
                    if (Hand1[i].AttackType == 1)
                    {
                        GameObject instCard = Instantiate(DistanceCardPrefab, Position, Quaternion.identity, CardSpawner.transform);
                        instCard.GetComponent<Card>().SetData(Hand1[i].Name, Hand1[i].Cost, Hand1[i].MoveCost, Hand1[i].AttackType, Hand1[i].Distance, Hand1[i].Attack, Hand1[i].AttackCost, Hand1[i].Hp, Hand1[i].Armour);

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
            if (j == 2)
            {
                Position = new Vector3(0, -3.5f, 0);

                if (CardCount2 % 2 == 0)
                {
                    Position.x = -0.05f;
                }
                else { Position.x = 0; }

                for (int i = 0; i < CardCount2; i++)
                {
                    if (Hand2[i].AttackType == 0)
                    {
                        GameObject instCard = Instantiate(MeleeCardPrefab, Position, Quaternion.identity, CardSpawner.transform);
                        instCard.GetComponent<Card>().SetData(Hand2[i].Name, Hand2[i].Cost, Hand2[i].MoveCost, Hand2[i].AttackType, Hand2[i].Distance, Hand2[i].Attack, Hand2[i].AttackCost, Hand2[i].Hp, Hand2[i].Armour);



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
                    if (Hand2[i].AttackType == 1)
                    {
                        GameObject instCard = Instantiate(DistanceCardPrefab, Position, Quaternion.identity, CardSpawner.transform);
                        instCard.GetComponent<Card>().SetData(Hand2[i].Name, Hand2[i].Cost, Hand2[i].MoveCost, Hand2[i].AttackType, Hand2[i].Distance, Hand2[i].Attack, Hand2[i].AttackCost, Hand2[i].Hp, Hand2[i].Armour);

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
        }
        public void HideCards(int j)
        {
            if (j == 1)
            {
                for (int n = 0; n < CardCount1; n++)
                {
                    Destroy(CardSpawner.transform.GetChild(n).gameObject);
                }
            }
            if (j == 2)
            {
                for (int n = 0; n < CardCount2; n++)
                {
                    Destroy(CardSpawner.transform.GetChild(n).gameObject);
                }
            }
        }
    }
}
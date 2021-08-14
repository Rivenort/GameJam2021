using BestGameEver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    public class CardsManager : MonoBehaviour
    {
        public CardTemplate[] Deck1;
        public CardTemplate[] Deck2;

        private CardTemplate tempGO;

        int Count1;
        int Count2;

        private void Start()
        {
            Shuffle(1);
            Shuffle(2);
            Count1 = 0;
            Count2=0;
        }

        public CardTemplate DrawCard(int j)
        {
            CardTemplate temp;

            if (j == 1)
            {

                temp = Deck1[Count1];
                Count1++;
                if (Count1 == Deck1.Length)
                {
                    Shuffle(1);
                    Count1 = 0;
                }
            }

           else
            {

                temp = Deck2[Count2];
                Count2++;
                if (Count2 == Deck2.Length)
                {
                    Shuffle(2);
                    Count2 = 0;
                }
            }

            return temp;
        }


        public void Shuffle(int j)
        {
            if (j == 1)
            {

                for (int i = 0; i < Deck1.Length; i++)
                {
                    int rnd = Random.Range(0, Deck1.Length);
                    tempGO = Deck1[rnd];
                    Deck1[rnd] = Deck1[i];
                    Deck1[i] = tempGO;
                }
            }
            else
            {
                for (int i = 0; i < Deck2.Length; i++)
                {
                    int rnd = Random.Range(0, Deck2.Length);
                    tempGO = Deck2[rnd];
                    Deck2[rnd] = Deck2[i];
                    Deck2[i] = tempGO;
                }

            }
        }
    }
}
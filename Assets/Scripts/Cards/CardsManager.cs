using BestGameEver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    public class CardsManager : MonoBehaviour
    {
        public CardTemplate[] Deck;
        private CardTemplate tempGO;

        int Count;

        private void Start()
        {
            Count = 0;
            Shuffle();
        }

        public CardTemplate DrawCard()
        {
            CardTemplate temp = Deck[Count];
            Count++;
            if(Count == Deck.Length)
            {
                Shuffle();
                Count = 0;
            }
            return temp;
        }
        
        void Test()
        {
            for (int i = 0 ; i < Deck.Length; i++){
                print(Deck[i].Name);
            }
        }
        public void Shuffle()
        {
            for (int i = 0; i < Deck.Length; i++)
            {
                int rnd = Random.Range(0, Deck.Length);
                tempGO = Deck[rnd];
                Deck[rnd] = Deck[i];
                Deck[i] = tempGO;
            }
        }
    }
}
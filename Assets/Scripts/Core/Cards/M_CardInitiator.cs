using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_CardInitiator : MonoBehaviour
    {
        private static M_CardInitiator s_instance = null;

        public List<CardTemplate> deck;
        public GameObject cardMelee;
        public GameObject cardRange;
        public Transform spawnPoint;

        public Transform cardsPlayer1;
        public Transform cardsPlayer2;
        public Transform cardsHandLeftPoint;
        public Transform cardsHandRightPoint;

        void Start()
        {
            if (s_instance != null && s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;
        }


        private ICard InitRand()
        {
            int rand = UnityEngine.Random.Range(0, deck.Count);
            CardTemplate templ = deck[rand];
            GameObject obj = null;
            if (templ.AttackType == 0) // melee
                obj = GameObject.Instantiate(cardMelee);
            else
                obj = GameObject.Instantiate(cardRange);
            obj.transform.position = spawnPoint.transform.position;

            PlayerType player = M_GamePlayManager.SGetCurrentPlayer();
            if (player == PlayerType.PLAYER_ONE)
                obj.transform.SetParent(cardsPlayer1);
            if (player == PlayerType.PLAYER_TWO)
                obj.transform.SetParent(cardsPlayer2);
            return obj.GetComponent<ICard>();
        }

        public static ICard SInitRand()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.InitRand();
        }

        public static Vector3 SGetHandLeftPoint()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.cardsHandLeftPoint.position;
        }

        public static Vector3 SGetHandRightPoint()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.cardsHandRightPoint.position;
        }

        private Vector3[] GetPointsForShownCards(int countCards)
        {
            Vector3[] res = new Vector3[countCards];
            if (countCards == 1)
            {
                float diffX = cardsHandRightPoint.position.x - cardsHandLeftPoint.position.x;
                Vector3 vec0 = cardsHandLeftPoint.position;
                vec0.x += diffX;
                return new Vector3[] { vec0 };
            }
            float distX = (cardsHandRightPoint.position.x - cardsHandLeftPoint.position.x)/(countCards - 1);
            for (int i = 0; i < countCards; i++)
            {
                Vector3 temp = cardsHandLeftPoint.position;
                temp.x += (distX * i);
                res[i] = temp;
            }
            return res;
        }

        public static Vector3[] SGetPointsForShownCards(int countCards)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.GetPointsForShownCards(countCards);
        }
    }

}
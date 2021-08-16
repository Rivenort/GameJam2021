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
        public Transform cardsHiddenP1;
        public Transform cardsHiddenP2;

        private HashSet<string> m_cardsBeenP1 = new HashSet<string>();
        private HashSet<string> m_cardsBeenP2 = new HashSet<string>();

        void Awake()
        {
            if (s_instance != null && s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;
        }


        private ICard InitRand()
        {
            if (m_cardsBeenP1.Count == deck.Count)
                m_cardsBeenP1.Clear();
            if (m_cardsBeenP2.Count == deck.Count)
                m_cardsBeenP2.Clear();

            PlayerType player = M_GamePlayManager.SGetCurrentPlayer();
            List<int> randFrom = new List<int>();
            switch (player)
            {
                case PlayerType.PLAYER_ONE:
                    {
                        for (int i = 0; i < deck.Count; i++)
                        {
                            if (!m_cardsBeenP1.Contains(deck[i].Name))
                                randFrom.Add(i);
                        }
                    } break;
                case PlayerType.PLAYER_TWO:
                    {
                        for (int i = 0; i < deck.Count; i++)
                        {
                            if (!m_cardsBeenP2.Contains(deck[i].Name))
                                randFrom.Add(i);
                        }
                    } break;
            }


            int randIndex = UnityEngine.Random.Range(0, randFrom.Count);
            CardTemplate templ = deck[randFrom[randIndex]];
            
            switch (player)
            {
                case PlayerType.PLAYER_ONE:
                    m_cardsBeenP1.Add(templ.Name);
                    break;
                case PlayerType.PLAYER_TWO:
                    m_cardsBeenP2.Add(templ.Name);
                    break;    
            }

            GameObject obj = null;
            if (templ.AttackType == 0) // melee
                obj = GameObject.Instantiate(cardMelee);
            else
                obj = GameObject.Instantiate(cardRange);
            

            
            if (player == PlayerType.PLAYER_ONE)
                obj.transform.SetParent(cardsPlayer1, true);
            if (player == PlayerType.PLAYER_TWO)
                obj.transform.SetParent(cardsPlayer2, true);

            ICard card = obj.GetComponent<ICard>();
            card.SetData(templ);
            return card;
        }

        public static ICard SInitRand()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.InitRand();
        }


        public static Vector3 SGetSpawnPoint()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.spawnPoint.position;
        }

        private void ShowCards(PlayerType player)
        {

            if (player == PlayerType.PLAYER_ONE)
            {
                for (int i = cardsHiddenP1.childCount - 1; i >= 0; i--)
                {
                    Transform card = cardsHiddenP1.GetChild(i);
                    card.SetParent(cardsPlayer1);
                }
            } else if (player == PlayerType.PLAYER_TWO)
            {
                for (int i = cardsHiddenP2.childCount - 1; i >= 0; i--)
                {
                    Transform card = cardsHiddenP2.GetChild(i);
                    card.SetParent(cardsPlayer2);
                }
            }
        }

        private void HideCards(PlayerType player)
        {
            if (player == PlayerType.PLAYER_ONE)
            {
                for (int i = cardsPlayer1.childCount - 1; i >= 0; i--)
                {
                    Transform card = cardsPlayer1.GetChild(i);
                    card.SetParent(cardsHiddenP1);
                }
            }
            else if (player == PlayerType.PLAYER_TWO)
            {
                for (int i = cardsPlayer2.childCount - 1; i >= 0; i--)
                {
                    Transform card = cardsPlayer2.GetChild(i);
                    card.SetParent(cardsHiddenP2);
                }
            }
        }

        public static void SShowCards(PlayerType player)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.ShowCards(player);
        }

        public static void SHideCards(PlayerType player)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.HideCards(player);
        }

    }

}
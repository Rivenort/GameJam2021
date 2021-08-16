using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class M_CardManager : UT_IDoOnGameStart
    {
        private static M_CardManager s_instance = null;
        private static readonly object s_lock = new object();

        private const string OBJECTNAME_CARDS_P1 = "UI/CardsP1";
        private const string OBJECTNAME_CARDS_P2 = "UI/CardsP2";
        private Transform m_groupPlayer1;
        private Transform m_groupPlayer2;

        private Dictionary<Guid, ICard> m_player1;
        private Dictionary<Guid, ICard> m_player2;

        private int m_maxCards = 4;

        private M_CardManager()
        {
            m_player1 = new Dictionary<Guid, ICard>();
            m_player2 = new Dictionary<Guid, ICard>();
            m_groupPlayer1 = GameObject.Find(OBJECTNAME_CARDS_P1).transform;
            m_groupPlayer2 = GameObject.Find(OBJECTNAME_CARDS_P2).transform;
        }

        public static M_CardManager GetInstance()
        {
            lock (s_lock)
            {
                if (s_instance == null)
                    s_instance = new M_CardManager();
                return s_instance;
            }
        }

        private bool CanDrawACard(PlayerType player)
        {
            if (player == PlayerType.PLAYER_ONE)
                return m_player1.Count < m_maxCards;
            if (player == PlayerType.PLAYER_TWO)
                return m_player2.Count < m_maxCards;
            return false;
        }

        public static bool SCanDrawACard(PlayerType player)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.CanDrawACard(player);
        }

        private void DrawRandCard()
        {
            PlayerType player = M_GamePlayManager.SGetCurrentPlayer();
            ICard card = M_CardInitiator.SInitRand();
            if (player == PlayerType.PLAYER_ONE && m_player1.Count < m_maxCards)
                m_player1.Add(card.GetId(), card);
            if (player == PlayerType.PLAYER_TWO && m_player2.Count < m_maxCards)
                m_player2.Add(card.GetId(), card);
        }

        public static void SDrawRandCard()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.DrawRandCard();
        }


        public void OnGameStart()
        {

        }

        private void ShowCards()
        {
            M_CardInitiator.SShowCards(M_GamePlayManager.SGetCurrentPlayer());
        }

        private void HideCards()
        {
            M_CardInitiator.SHideCards(M_GamePlayManager.SGetCurrentPlayer());
        }

        public static void SShowCards()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.ShowCards();
        }

        public static void SHideCards()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.HideCards();
        }

        private void CardWasUsed(Guid id)
        {
            PlayerType player = M_GamePlayManager.SGetCurrentPlayer();
            switch (player)
            {
                case PlayerType.PLAYER_ONE:
                {
                    if (m_player1.ContainsKey(id))
                    {
                        m_player1.Remove(id);
                    }
                } break;
                case PlayerType.PLAYER_TWO:
                    {
                        if (m_player2.ContainsKey(id))
                        {
                            m_player2.Remove(id);
                        }
                    }
                    break;
            }
        }

        public static void SCardWasUsed(Guid id)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.CardWasUsed(id);
        }
    }

}
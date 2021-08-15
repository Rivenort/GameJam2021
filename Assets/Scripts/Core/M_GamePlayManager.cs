using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    public delegate void OnNewTurn(PlayerType player);
    public delegate void OnGameStart();
    public delegate void OnCardDraw();
    public delegate void OnActionPhase();

    public class M_GamePlayManager : UT_IDoOnGameStart
    {
        private static M_GamePlayManager s_instance = null;
        private static readonly object s_lock = new object();

        private event OnNewTurn m_eventOnNewTurn;
        private event OnGameStart m_eventOnGameStart;
        private event OnCardDraw m_eventOnCardDraw;
        private event OnActionPhase m_eventOnActionPhase;

        private PlayerType m_turn;

        private int m_player1Points = 30;
        private int m_player2Points = 30;

        private M_GamePlayManager()
        {

        }

        public static M_GamePlayManager GetInstance()
        {
            lock (s_lock)
            {
                if (s_instance == null)
                    s_instance = new M_GamePlayManager();
                return s_instance;
            }
        }

        public static void SAddListener_OnNewTurn(OnNewTurn func)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_eventOnNewTurn += func;
        }

        public static void SAddListener_OnGameStart(OnGameStart func)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_eventOnGameStart += func;
        }

        public static void SAddListener_OnCardDraw(OnCardDraw func)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_eventOnCardDraw += func;
        }

        public static void SAddListener_OnActionPhase(OnActionPhase func)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_eventOnActionPhase += func;
        }

        public void OnGameStart()
        {
            m_eventOnGameStart?.Invoke();
            m_eventOnNewTurn?.Invoke(m_turn);
        }

        public static void SScreenNewTurnFinished()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            if (M_CardManager.SCanDrawACard(s_instance.m_turn))
                s_instance.m_eventOnCardDraw?.Invoke();
            else
                s_instance.m_eventOnActionPhase?.Invoke();
        }

        public static PlayerType SGetCurrentPlayer()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.m_turn;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    public delegate void OnNewTurn(PlayerType player);
    public delegate void OnGameStart();
    public delegate void OnCardDraw();
    public delegate void OnActionPhase();
    public delegate void OnPointsChange(PlayerType player, int points);
    public delegate void OnCardsHide(PlayerType player);
    public delegate void OnCardsShow(PlayerType player);

    public class M_GamePlayManager : UT_IDoOnGameStart
    {
        private static M_GamePlayManager s_instance = null;
        private static readonly object s_lock = new object();

        private event OnNewTurn m_eventOnNewTurn;
        private event OnGameStart m_eventOnGameStart;
        private event OnCardDraw m_eventOnCardDraw;
        private event OnActionPhase m_eventOnActionPhase;
        private event OnPointsChange m_eventOnPointsChange;
        private event OnCardsHide m_eventOnCardsHide;
        private event OnCardsShow m_eventOnCardsShow;

        private PlayerType m_turn;

        private int m_player1Points = 10;
        private int m_player2Points = 10;
        private int m_pointsOnNewTurn = 10;

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

        public static void SAddListener_OnPointsChange(OnPointsChange func)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_eventOnPointsChange += func;
        }

        public static void SAddListener_OnCardsHide(OnCardsHide func)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_eventOnCardsHide += func;
        }

        public static void SAddListener_OnCardsShow(OnCardsShow func)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_eventOnCardsShow += func;
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

        private void DeployingMob(string prefabName)
        {
            if (m_turn == PlayerType.PLAYER_ONE)
            {
                M_MobInitalizer.SSetP1Default(prefabName);
                M_SpawnManager.SExposeSpawnPoints(PlayerType.PLAYER_ONE);
            }
            if (m_turn == PlayerType.PLAYER_TWO)
            {
                M_MobInitalizer.SSetP2Default(prefabName);
                M_SpawnManager.SExposeSpawnPoints(PlayerType.PLAYER_TWO);
            }
        }

        public static void SDeployMob(string prefabName)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.DeployingMob(prefabName);
        }

        public static int SGetCurrentPlayerPoints()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            if (s_instance.m_turn == PlayerType.PLAYER_ONE)
                return s_instance.m_player1Points;
            if (s_instance.m_turn == PlayerType.PLAYER_TWO)
                return s_instance.m_player2Points;
            return 0;
        }

        public static int SGetPlayer1Points()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.m_player1Points;
        }

        public static int SGetPlayer2Points()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            return s_instance.m_player2Points;
        }

        public static void SAddToPlayer1Points(int amount)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_player1Points += amount;
            s_instance.m_player1Points = Mathf.Max(0, s_instance.m_player1Points);
            s_instance.m_eventOnPointsChange?.Invoke(PlayerType.PLAYER_ONE, s_instance.m_player1Points);
        }

        public static void SAddToPlayer2Points(int amount)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_player2Points += amount;
            s_instance.m_player2Points = Mathf.Max(0, s_instance.m_player2Points);
            s_instance.m_eventOnPointsChange?.Invoke(PlayerType.PLAYER_TWO, s_instance.m_player2Points);
        }

        public static void SAddToCurrentPlayer(int amount)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            switch (s_instance.m_turn)
            {
                case PlayerType.PLAYER_ONE:
                    SAddToPlayer1Points(amount);
                    break;
                case PlayerType.PLAYER_TWO:
                    SAddToPlayer2Points(amount);
                    break;
            }
        }

        private void NewTurn()
        {
            m_eventOnCardsHide?.Invoke(m_turn);
            M_MobManager.SHideUI();

            if (m_turn == PlayerType.PLAYER_ONE)
                m_turn = PlayerType.PLAYER_TWO;
            else if (m_turn == PlayerType.PLAYER_TWO)
                m_turn = PlayerType.PLAYER_ONE;

            switch (m_turn)
            {
                case PlayerType.PLAYER_ONE:
                    m_player1Points += m_pointsOnNewTurn;
                    break;
                case PlayerType.PLAYER_TWO:
                    m_player2Points += m_pointsOnNewTurn;
                    break;
            }


            m_eventOnPointsChange?.Invoke(m_turn, SGetCurrentPlayerPoints());
            m_eventOnNewTurn?.Invoke(m_turn);
        }

        public static void SNewTurn()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.NewTurn();
        }


        private void ShowCards()
        {
            M_CardManager.SShowCards();
            m_eventOnCardsShow?.Invoke(m_turn);
        }

        public static void SShowCards()
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.ShowCards();
        }
    }
}

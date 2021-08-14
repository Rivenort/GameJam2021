using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class MobUI : MonoBehaviour
    {
        public GameObject uiTopPanel;

        public Button uiBtnGoRight;
        public Button uiBtnGoLeft;
        public Button uiBtnGoUp;
        public Button uiBtnGoDown;
        public Button uiBtnAttack;
        public TMP_Text uiTextHealth;
        public TMP_Text uiTextAttack;

        private bool m_actionIsRunning = false;
        private bool m_disabled = false;

        private IMob m_mob = null;

        void Start()
        {
            if (uiTopPanel == null ||
                uiBtnGoRight == null ||
                uiBtnGoLeft == null ||
                uiBtnGoUp == null ||
                uiBtnGoDown == null ||
                uiBtnAttack == null ||
                uiTextAttack == null ||
                uiTextHealth == null)
                throw new CE_ComponentNotFullyInitialized();
            m_mob = GetComponentInParent<IMob>();
            if (m_mob == null)
                throw new CE_ExpectedElementNotFound("Component: " + typeof(IMob).Name + " was not found in the parent obj!");
            SetupButtons();
        }


        private void SetupButtons()
        {

            uiBtnGoRight.onClick.RemoveAllListeners();
            uiBtnGoLeft.onClick.RemoveAllListeners();
            uiBtnGoUp.onClick.RemoveAllListeners();
            uiBtnGoDown.onClick.RemoveAllListeners();
            uiBtnAttack.onClick.RemoveAllListeners();

            uiBtnGoRight.onClick.AddListener(ActionGoRight);
            uiBtnGoLeft.onClick.AddListener(ActionGoLeft);
            uiBtnGoUp.onClick.AddListener(ActionGoUp);
            uiBtnGoDown.onClick.AddListener(ActionGoDown);
            uiBtnAttack.onClick.AddListener(ActionAttack);
        }

        public void OnMobClick()
        {
            if (m_disabled)
                return;
            if (m_actionIsRunning)
            {
                Debug.Log("Action is running..");
                return;
            }

            M_MobManager.SSetChoosenMob(m_mob);

            // Check movement availability, then disable buttons
            if (M_MapManager.SIsAvailable(m_mob.GetRootPosition(), Directory.RIGHT))
                uiBtnGoRight.interactable = true;
            else
                uiBtnGoRight.interactable = false;

            if (M_MapManager.SIsAvailable(m_mob.GetRootPosition(), Directory.LEFT))
                uiBtnGoLeft.interactable = true;
            else
                uiBtnGoLeft.interactable = false;

            if (M_MapManager.SIsAvailable(m_mob.GetRootPosition(), Directory.UP))
                uiBtnGoUp.interactable = true;
            else
                uiBtnGoUp.interactable = false;

            if (M_MapManager.SIsAvailable(m_mob.GetRootPosition(), Directory.DOWN))
                uiBtnGoDown.interactable = true;
            else
                uiBtnGoDown.interactable = false;

            // Check attack availability
            IMob enemy = null;
            if (m_mob.GetStats().GetAttackType() == AttackType.MELEE)
                enemy = M_MapManager.SGetEnemyForMelee(m_mob.GetPlayer(), m_mob.GetRootPosition());
            if (m_mob.GetStats().GetAttackType() == AttackType.RANGE)
                enemy = M_MapManager.SGetEnemyForRanger(m_mob.GetPlayer(), m_mob.GetRootPosition(), m_mob.GetStats().GetRange());

            if (enemy != null)
            {
                uiBtnAttack.interactable = true;

                if (m_mob.GetPlayer() == PlayerType.PLAYER_ONE)
                    uiBtnGoRight.interactable = false;
                else if (m_mob.GetPlayer() == PlayerType.PLAYER_TWO)
                    uiBtnGoLeft.interactable = false;
            }
            else
                uiBtnAttack.interactable = false;


            RefreshStatWidgets();
            HighlightGrid();
            if (uiTopPanel.gameObject.activeSelf)
                ClosePanel();
            else
                uiTopPanel.gameObject.SetActive(true);
        }

        private void RefreshStatWidgets()
        {
            uiTextHealth.text = m_mob.GetStats().GetHp().ToString();
            uiTextAttack.text = m_mob.GetStats().GetAttack().ToString();
        }

        private void ActionGoRight()
        {
            m_mob.PlayAnimWalk(true);
            m_actionIsRunning = true;
            m_mob.PerformGoRight(OnActionComplete);
            ClosePanel();
        }

        private void ActionGoLeft()
        {
            m_mob.PlayAnimWalk(true);
            m_actionIsRunning = true;
            m_mob.PerformGoLeft(OnActionComplete);
            ClosePanel();
        }

        private void ActionGoUp()
        {
            m_mob.PlayAnimWalk(true);
            m_actionIsRunning = true;
            m_mob.PerformGoUp(OnActionComplete);
            ClosePanel();
        }

        private void ActionGoDown()
        {
            m_mob.PlayAnimWalk(true);
            m_actionIsRunning = true;
            m_mob.PerformGoDown(OnActionComplete);
            ClosePanel();
        }

        private void ActionAttack()
        {
            m_mob.PlayAnimAttack();
            m_actionIsRunning = true;
            m_mob.PerformAttack(OnActionComplete);
            ClosePanel();
        }

        private void OnActionComplete()
        {
            Debug.Log("Action completed.");
            m_mob.PlayAnimWalk(false);
            m_actionIsRunning = false;
            M_MainManager.SCallOnMobActionCompleted(m_mob);
        }

        public void ClosePanel()
        {
            uiTopPanel.gameObject.SetActive(false);
            M_MapHelper.SUnHighlightAll();
        }

        private void HighlightGrid()
        {
            AttackType attackType = m_mob.GetStats().GetAttackType();
            int dir = 1;
            if (m_mob.GetPlayer() == PlayerType.PLAYER_TWO)
                dir = -1;

            if (attackType == AttackType.MELEE)
            {
                Vector3Int cellPos = M_MapManager.SWorldPosToGridCell(m_mob.GetRootPosition());
                cellPos.x += dir;
                M_MapHelper.SHighligh(cellPos);
               
            }
            if (attackType == AttackType.RANGE)
            {
                Vector3Int cellPos = M_MapManager.SWorldPosToGridCell(m_mob.GetRootPosition());

                for (int i = 0; i < m_mob.GetStats().GetRange(); i++)
                {
                    Vector3Int tempCell = cellPos;
                    tempCell.x += ((i + 1) * dir);
                    M_MapHelper.SHighligh(tempCell);
                }
            }
        }

        public void DisableUI()
        {
            m_disabled = true;
        }

        public void EnableUI()
        {
            m_disabled = false;
        }
    }

}
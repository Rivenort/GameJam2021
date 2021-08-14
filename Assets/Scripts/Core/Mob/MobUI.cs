using System.Collections;
using System.Collections.Generic;
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

        public Button uiBtnGoLeft;
        public Button uiBtnGoRight;
        public Button uiBtnGoUp;
        public Button uiBtnGoDown;

        private bool m_actionIsRunning = false;

        private IMob m_mob = null;

        void Start()
        {
            if (uiTopPanel == null ||
                uiBtnGoLeft == null ||
                uiBtnGoRight == null ||
                uiBtnGoUp == null ||
                uiBtnGoDown == null)
                throw new CE_ComponentNotFullyInitialized();
            m_mob = GetComponentInParent<IMob>();
            if (m_mob == null)
                throw new CE_ExpectedElementNotFound("Component: " + typeof(IMob).Name + " was not found in the parent obj!");
            SetupButtons();
        }


        private void SetupButtons()
        {
            uiBtnGoLeft.onClick.RemoveAllListeners();
            uiBtnGoRight.onClick.RemoveAllListeners();
            uiBtnGoUp.onClick.RemoveAllListeners();
            uiBtnGoDown.onClick.RemoveAllListeners();
            uiBtnGoLeft.onClick.AddListener(ActionGoLeft);
            uiBtnGoRight.onClick.AddListener(ActionGoRight);
            uiBtnGoUp.onClick.AddListener(ActionGoUp);
            uiBtnGoDown.onClick.AddListener(ActionGoDown);
        }

        public void OnMobClick()
        {
            if (m_actionIsRunning)
                return;

            // Check movement availability, then disable buttons
            if (M_MapManager.SIsAvailable(m_mob.GetRootPosition(), Directory.LEFT))
                uiBtnGoLeft.interactable = true;
            else
                uiBtnGoLeft.interactable = false;

            if (M_MapManager.SIsAvailable(m_mob.GetRootPosition(), Directory.RIGHT))
                uiBtnGoRight.interactable = true;
            else
                uiBtnGoRight.interactable = false;

            if (M_MapManager.SIsAvailable(m_mob.GetRootPosition(), Directory.UP))
                uiBtnGoUp.interactable = true;
            else
                uiBtnGoUp.interactable = false;

            if (M_MapManager.SIsAvailable(m_mob.GetRootPosition(), Directory.DOWN))
                uiBtnGoDown.interactable = true;
            else
                uiBtnGoDown.interactable = false;

            uiTopPanel.gameObject.SetActive(!uiTopPanel.gameObject.activeSelf);
        }

        private void ActionGoLeft()
        {
            m_mob.PerformGoLeft(OnActionComplete);
            uiTopPanel.gameObject.SetActive(false);
            m_actionIsRunning = true;
        }

        private void ActionGoRight()
        {
            m_mob.PerformGoRight(OnActionComplete);
            uiTopPanel.gameObject.SetActive(false);
            m_actionIsRunning = true;
        }

        private void ActionGoUp()
        {
            m_mob.PerformGoUp(OnActionComplete);
            uiTopPanel.gameObject.SetActive(false);
            m_actionIsRunning = true;
        }

        private void ActionGoDown()
        {
            m_mob.PerformGoDown(OnActionComplete);
            uiTopPanel.gameObject.SetActive(false);
            m_actionIsRunning = true;
        }

        private void OnActionComplete()
        {
            Debug.Log("Action completed.");
            m_actionIsRunning = false;
        }
    }

}
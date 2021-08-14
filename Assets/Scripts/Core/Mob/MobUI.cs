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
            uiTopPanel.gameObject.SetActive(!uiTopPanel.gameObject.activeSelf);
        }

        private void ActionGoLeft()
        {
            m_mob.PerformGoLeft(OnActionComplete);
        }

        private void ActionGoRight()
        {
            m_mob.PerformGoRight(OnActionComplete);
        }

        private void ActionGoUp()
        {
            m_mob.PerformGoUp(OnActionComplete);
        }

        private void ActionGoDown()
        {
            m_mob.PerformGoDown(OnActionComplete);
        }

        private void OnActionComplete()
        {
            Debug.Log("Action completed.");
        }
    }

}
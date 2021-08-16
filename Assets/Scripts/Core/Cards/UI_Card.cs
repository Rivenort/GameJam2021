using Error300.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WelcomeToMyCave;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UI_Card : MonoBehaviour, ICard
    {

        public TMP_Text uiTextCost;
        public TMP_Text uiTextAttack;
        public TMP_Text uiTextArmour;
        public TMP_Text uiTextHp;
        public TMP_Text uiTextTitle;
        public TMP_Text uiTextRange;
        public Image uiIcon;
        private Button m_uiButton;
        private UI_FadeOut2 m_fadeOut;

        public CardTemplate cardTemplate;

        private Guid m_id;



        void Start()
        {
            if (uiTextArmour == null ||
                uiTextCost == null ||
                uiTextAttack == null ||
                uiTextHp == null ||
                uiTextTitle == null ||
                uiTextRange == null)
                throw new CE_ComponentNotFullyInitialized();

            if (m_id == Guid.Empty)
                m_id = Guid.NewGuid();
            m_uiButton = GetComponentInChildren<Button>();
            m_uiButton.onClick.AddListener(OnClick);
            m_fadeOut = GetComponent<UI_FadeOut2>();
        }

        public CardTemplate GetData()
        {
            return cardTemplate;
        }

        public Guid GetId()
        {
            if (m_id == Guid.Empty)
                m_id = Guid.NewGuid();
            return m_id;
        }


        public void SetData(CardTemplate data)
        {
            uiTextCost.text = data.Cost.ToString();
            uiTextRange.text = data.Distance.ToString();
            uiTextAttack.text = data.Attack.ToString();
            uiTextHp.text = data.Hp.ToString();
            uiTextTitle.text = data.Name.ToString();
            uiTextArmour.text = data.Armour.ToString();
            uiIcon.sprite = data.Character;
            this.cardTemplate = data;
        }

        private void OnClick()
        {
            if (cardTemplate.Cost > M_GamePlayManager.SGetCurrentPlayerPoints())
                return;
            
            PlayerType player = M_GamePlayManager.SGetCurrentPlayer();
            if (!M_MapManager.SAreAnySpawnsFree(player))
            {
                UI_ScreenMsgFreeSpace.SShow();
                return;
            }

            M_GamePlayManager.SAddToCurrentPlayer(-cardTemplate.Cost);

            if (player == PlayerType.PLAYER_ONE)
                M_GamePlayManager.SDeployMob(cardTemplate.prefabNameP1);
            else if (player == PlayerType.PLAYER_TWO)
                M_GamePlayManager.SDeployMob(cardTemplate.prefabNameP2);

            UI_ButtonNextTurn.SSetInteractible(false);
            UI_ButtonShowCards.SSetInteractible(false);

            m_fadeOut?.Run();
            if (m_fadeOut == null)
                OnFadeOutFinished();
        }

        private void OnFadeOutFinished()
        {
            M_CardManager.SCardWasUsed(m_id);
            GameObject.Destroy(this);
        }
    }

}
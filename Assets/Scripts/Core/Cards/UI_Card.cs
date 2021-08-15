using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class UI_ButtonShowCards : MonoBehaviour
    {
        public Sprite texCardsHidden;
        public Sprite texCardsShown;
        private Button m_button;
        private Image m_image;

        private bool m_areCardsHidden = true;

        void Start()
        {
            m_image = GetComponent<Image>();
            m_button = GetComponent<Button>();

            if (m_areCardsHidden)
                m_image.sprite = texCardsHidden;
            else
                m_image.sprite = texCardsShown;
            m_button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            if (m_areCardsHidden)
            {
                m_image.sprite = texCardsHidden;
                m_areCardsHidden = false;
                M_CardManager.SShowCards();
            } else
            {
                m_image.sprite = texCardsShown;
                m_areCardsHidden = true;
            }
        }

        void Update()
        {

        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
    /// <summary>
    /// [Component Singleton]
    /// @author Rivenort
    /// </summary>
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class UI_ButtonShowCards : MonoBehaviour
    {
        private static UI_ButtonShowCards s_instance = null;

        public Sprite texCardsHidden;
        public Sprite texCardsShown;
        private Button m_button;
        private Image m_image;

        private bool m_areCardsHidden = true;
        private bool m_wasFirstUpdate = false;

        void Start()
        {
            if (s_instance != null && s_instance != this)
                throw new CE_ComponentSingletonReinitialized();
            s_instance = this;

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
                M_CardManager.SHideCards();
            }
        }

        public static void SSetInteractible(bool val)
        {
            if (s_instance == null)
                throw new CE_SingletonNotInitialized();
            s_instance.m_button.interactable = val;
        }

        void Update()
        {
            if (!m_wasFirstUpdate)
            {
                M_GamePlayManager.SAddListener_OnCardsHide((p) => {
                    m_image.sprite = texCardsShown;
                    m_areCardsHidden = true;
                    M_CardManager.SHideCards();
                });
                M_GamePlayManager.SAddListener_OnCardsShow((p) => {
                    m_image.sprite = texCardsHidden;
                    m_areCardsHidden = false;
                    
                });
                m_wasFirstUpdate = true;
            }
        }
    }

}
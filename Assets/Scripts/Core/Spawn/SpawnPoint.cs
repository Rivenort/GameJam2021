using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        private ClickableObject m_clickableObject;
        private UT_ScaleUpDown m_scaleAnim;

        [SerializeField]
        private GameObject m_model;

        [SerializeField]
        private PlayerType m_player;

        private Guid m_id;

        void Start()
        {
            if (m_id == Guid.Empty)
                m_id = Guid.NewGuid();
            m_clickableObject = GetComponentInChildren<ClickableObject>();
            m_scaleAnim = GetComponent<UT_ScaleUpDown>();
            m_clickableObject.disabled = true;
            m_clickableObject.Func = OnClick;
            m_model.SetActive(false);
        }

        public Guid GetId()
        {
            if (m_id == Guid.Empty)
                m_id = Guid.NewGuid();
            return m_id;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        private void OnClick()
        {
            if (m_player == PlayerType.PLAYER_ONE)
                M_MobInitalizer.SInitP1(this);
            else if (m_player == PlayerType.PLAYER_TWO)
                M_MobInitalizer.SInitP2(this);
            M_SpawnManager.SInterruprtExposedSpawns();

            UI_ButtonNextTurn.SSetInteractible(true);
            UI_ButtonShowCards.SSetInteractible(true);
            M_CardManager.SShowCards();
        }

        public void Expose()
        {
            m_clickableObject.disabled = false;
            m_scaleAnim.Run();
            m_model.SetActive(true);
        }

        public void DisableExspose()
        {
            m_clickableObject.disabled = true;
            m_scaleAnim.Interrupt();
            m_model.SetActive(false);
        }

        public PlayerType GetPlayerType()
        {
            return m_player;
        }
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BestGameEver
{


    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class MobMotor : MonoBehaviour
    {
        [SerializeField]
        private float m_speed = 5;
        private bool m_isMoving = false;
        private Vector3 m_dest;
        private Vector3 m_dir;
        [SerializeField]
        private float m_offset = 0.2f;


        private Action m_onComplete;
        [SerializeField]
        private GameObject mainObject;

        void Start()
        {
            if (mainObject == null)
                throw new CE_ComponentNotFullyInitialized();

            Vector3 diff = M_MapManager.SGetDiffFromGrid(transform.position);
            mainObject.transform.position = mainObject.transform.position + diff;
        }


        void Update()
        {
            if (m_isMoving)
                Move();
        }

        private void Move()
        {
            float distLeft = Vector3.Distance(gameObject.transform.position, m_dest);
            //Debug.Log(distLeft + " GO: " + gameObject.transform.position + " Dest: " + m_dest);
            if (distLeft > m_offset)
            {
                Vector3 nextStep = m_dir * m_speed * Time.deltaTime + mainObject.transform.position;
                mainObject.transform.position = nextStep;
            } else
            {
                Vector3 diff = m_dest - gameObject.transform.position;
                mainObject.transform.position = mainObject.transform.position + diff;
                m_isMoving = false;
                m_onComplete?.Invoke();
            }
        }

        public void MoveRight(Action callback)
        {
            m_isMoving = true;
            m_onComplete = callback;
            m_dir = new Vector3(1, 0, 0);
            m_dest = GetDestination(1, 0);
            Debug.Log("MobMotor -> MoveRight(): Dest: " + m_dest + " Start: " + gameObject.transform.position + " Dir: " + m_dir);
        }

        public void MoveLeft(Action callback)
        {
            m_isMoving = true;
            m_onComplete = callback;
            m_dir = new Vector3(-1, 0, 0);
            m_dest = GetDestination(-1, 0);
            Debug.Log("MobMotor -> MoveLeft(): Dest: " + m_dest + " Start: " + gameObject.transform.position + " Dir: " + m_dir);
        }

        public void MoveUp(Action callback)
        {
            m_isMoving = true;
            m_onComplete = callback;
            m_dir = new Vector3(0, 1, 0);
            m_dest = GetDestination(0, 1);
            Debug.Log("MobMotor -> MoveUp(): Dest: " + m_dest + " Start: " + gameObject.transform.position + " Dir: " + m_dir);
        }

        public void MoveDown(Action callback)
        {
            m_isMoving = true;
            m_onComplete = callback;
            m_dir = new Vector3(0, -1, 0);
            m_dest = GetDestination(0, -1);
            Debug.Log("MobMotor -> MoveDown(): Dest: " + m_dest + " Start: " + gameObject.transform.position + " Dir: " + m_dir);
        }

        private Vector3 GetDestination(int cellXOffset, int cellYOffset)
        {
            Vector3Int currentCell = M_MapManager.SWorldPosToGridCell(gameObject.transform.position);
            Vector3Int destCell = new Vector3Int(currentCell.x + cellXOffset, currentCell.y + cellYOffset, 0);
            return M_MapManager.SGridCellToWorldPos(destCell);
        }

    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BestGameEver
{
    public class WinningPointManager : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI T_Name1;

        [SerializeField]
        TextMeshProUGUI T_Name2;

        public TurnManager turnManager;

        public GameObject Light1;
        public GameObject Light2;

        public GameObject Bar1;
        public GameObject Bar2;
        
        public int MaxPoints1 = 50;
        public int MaxPoints2 = 50;

        public int Points1 = 0;
        public int Points2 = 0;

        void Start()
        {
        }

        /*  void Update()
         {
             if (Points1 == MaxPoints1)
             {

             }

             if (Points2 == MaxPoints2)
             {

             }  

         }*/
        public void AddPoints(int j, int value)
        {
            if (j == 1)
            {
                Points1 += value;
            }
            if (j == 2)
            {
                Points2 += value;
            }
            UpdateUI();
        }

        public void UpdateUI()
        {
            if (turnManager.Player == 1)
            {
                Light1.SetActive(true);
                Light2.SetActive(false);
            }

            if (turnManager.Player == 2)
            {
                Light1.SetActive(false);
                Light2.SetActive(true);
            }

            T_Name1.text = Points1.ToString();
            T_Name2.text = Points2.ToString();

            Bar1.transform.localScale = new Vector3(0.0f, Points1 / MaxPoints1,0.0f);
            Bar2.transform.localScale = new Vector3(0.0f, Points2 / MaxPoints2, 0.0f);
        }
    }
}

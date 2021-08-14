using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
    public class SampleButton : MonoBehaviour
     {
        public HandManager handManager;
        public Button button;


		void Start()
		{
			Button btn = button.GetComponent<Button>();
			btn.onClick.AddListener(TaskOnClick);
		}

		void TaskOnClick()
		{
			handManager.AddNewCard();
		}

	}
}
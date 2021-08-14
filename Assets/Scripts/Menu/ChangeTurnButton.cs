using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
	public class ChangeTurnButton : MonoBehaviour
	{
		public Button button;
		public TurnManager turnManager;

		void Start()
		{
			Button btn = button.GetComponent<Button>();
			btn.onClick.AddListener(TaskOnClick);
		}

		void TaskOnClick()
		{
			turnManager.NextTurn();
		}

	}
}
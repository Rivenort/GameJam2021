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
		public HideCardsButton hideCardsButton;

		void Start()
		{
			Button btn = button.GetComponent<Button>();
			btn.onClick.AddListener(TaskOnClick);
		}

		void TaskOnClick()
		{
			if (hideCardsButton.isActiveAndEnabled == false)
			{

				hideCardsButton.Hide();
				turnManager.NextTurn();
			}
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
	public class ShowCardsButton : MonoBehaviour
	{
		public TurnManager turnManager;

		public HideCardsButton hideCardsButton;

		public GameObject gameObject;

		public HandManager handManager;
		public Button button;

		void Start()
		{
			Button btn = button.GetComponent<Button>();
			btn.onClick.AddListener(TaskOnClick);
		}

		void TaskOnClick()
		{
			if (turnManager.Player==1)
			{
				if (handManager.CardCount1 != 0)
				{
					handManager.ShowCards(1);
					Hide();
					hideCardsButton.Show();
				}
				else
				{
					print("you have no CARDS!!!");
				}
			}
			if (turnManager.Player == 2)
			{
				if (handManager.CardCount2 != 0)
				{
					handManager.ShowCards(2);
					Hide();
					hideCardsButton.Show();
				}
				else
				{
					print("you have no CARDS!!!");
				}
			}
		}
		public void Show()
        {
			gameObject.SetActive(true);
		}

		void Hide()
        {
			gameObject.SetActive(false);

		}
	}
}
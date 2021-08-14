using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver{
	public class HideCardsButton : MonoBehaviour
	{
		public ShowCardsButton showCardsButton;
		public GameObject hideButton;

		public TurnManager turnManager;
		public HandManager handManager;
		public Button button;

		void Start()
		{
			Button btn = button.GetComponent<Button>();
			btn.onClick.AddListener(TaskOnClick);
			Hide();
		}

		void TaskOnClick()
		{
			handManager.HideCards(turnManager.Player);
			Hide();
            showCardsButton.Show();
		}

		public void Show()
		{
			hideButton.SetActive(true);
		}
		public void Hide()
		{
			hideButton.SetActive(false);
		}
	}
}
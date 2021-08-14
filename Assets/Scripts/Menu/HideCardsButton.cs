using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver{
	public class HideCardsButton : MonoBehaviour
	{
		public ShowCardsButton showCardsButton;
		public GameObject hideButton;

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
			handManager.HideCards();
			Hide();
			showCardsButton.Show();
		}

		public void Show()
		{
			hideButton.SetActive(true);
		}
		void Hide()
		{
			hideButton.SetActive(false);
		}
	}
}
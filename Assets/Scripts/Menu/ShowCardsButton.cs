using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
	public class ShowCardsButton : MonoBehaviour
	{
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
			if (handManager.CardCount != 0)
			{
				handManager.ShowCards();
				Hide();
				hideCardsButton.Show();
			}
            else
            {
                print( "you have no CARDS!!!");
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
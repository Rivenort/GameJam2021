using BestGameEver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
	public class StartGameButton : MonoBehaviour
	{
		public Button button;

		void Start()
		{
			Button btn = button.GetComponent<Button>();
			btn.onClick.AddListener(TaskOnClick);
		}

		void TaskOnClick()
		{
			Debug.Log("You have clicked the button!");
			M_SceneLoaderManager.SLoadScene(1);
		}
	}
}

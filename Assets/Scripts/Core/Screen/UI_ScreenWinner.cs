using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BestGameEver
{
    /// <summary>
    /// @author Rivenort
    /// </summary>
    public class UI_ScreenWinner : MonoBehaviour
    {

        public TMP_Text textWinner;
        public TMP_Text textPlayer;
        public Button btnQuit;
        private bool m_wasFirstUpdate;
        public string textPlayer1;
        public string textPlayer2;
        public Color colorPlayer1;
        public Color colorPlayer2;

        void Start()
        {
            btnQuit.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }

        private void Update()
        {
            if (!m_wasFirstUpdate)
            {
                M_MobManager.SAddListener_OnAllPlayerMobsDestroyed((player) => {

                    if (player == PlayerType.PLAYER_ONE)
                    {
                        textWinner.color = colorPlayer2;
                        textPlayer.text = textPlayer2;
                        textPlayer.color = colorPlayer2;
                    } else
                    {
                        textWinner.color = colorPlayer1;
                        textPlayer.text = textPlayer1;
                        textPlayer.color = colorPlayer1;
                    }

                    gameObject.SetActive(true);
                    GetComponent<UI_FadeIn2>()?.Run();
                });
                gameObject.SetActive(false);
                m_wasFirstUpdate = true;
            }
        }

        

    }

}
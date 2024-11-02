using UnityEngine;
using UnityEngine.SceneManagement;

namespace AloneInInquisition.MenuSystem.PauseMenu
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject panel;
        [SerializeField] private bool pauseGame;

        public bool PauseGame
        {
            get => pauseGame;
            set
            {
                pauseGame = value;
                Time.timeScale = pauseGame ? 0 : 1;
            }
        }

        public void Pause()
        {
            panel.SetActive(true);
            PauseGame = true;
        }

        public void Resume()
        {
            panel.SetActive(false);
            PauseGame = false;
        }

        public void ExitGame()
        {
            Application.Quit();
            Debug.Log("Exit pressed!");
        }

        public void GoToMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
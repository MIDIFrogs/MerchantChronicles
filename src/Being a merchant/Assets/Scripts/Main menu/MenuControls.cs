using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("SampleLevel", LoadSceneMode.Single);
        Time.timeScale = 1;
    }

    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");
    }

    public void MainMenuPressed()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
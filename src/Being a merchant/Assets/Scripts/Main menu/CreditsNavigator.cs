using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsNavigator : MonoBehaviour
{
    public void NavigateToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NavigateToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
  public void PlayButton()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Level1Button()
    {
        SceneManager.LoadScene("Scene1");
    }
}

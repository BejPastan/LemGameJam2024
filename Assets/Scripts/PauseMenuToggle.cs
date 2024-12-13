using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuToggle : MonoBehaviour
{
    static Transform pauseMenu;
    static bool isPaused = false;

    private void Start()
    {
        pauseMenu = transform.Find("PauseMenu");
    }

    public static void TogglePauseMenu()
    {
        if(isPaused)
        {
            isPaused = false;
            pauseMenu.gameObject.SetActive(false);
        }
        else
        {
            isPaused = true;
            pauseMenu.gameObject.SetActive(true);
        }
    }

    public static void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void ExitGame()
    {
        Application.Quit();
    }

    public static void StartGame()
    {
        SceneManager.LoadScene("Level1Testowy");
    }
}

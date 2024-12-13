using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuToggle : MonoBehaviour
{
    static Transform pauseMenu;
    static bool isPaused = false;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        /* Freezes the game */
        Time.timeScale = 0f;
        GameIsPaused = true;

        /* Desired pause settings */
        //PauseMenuUI.SetActive(true);
        ////Time.timeScale = 1f;
        //GameIsPaused = true;
    }

    public void LoadSettings()
    {
        Debug.Log("Loading Settings...");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
        //Debug.Log("Loading Main Menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
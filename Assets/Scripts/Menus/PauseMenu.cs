using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public bool GameIsPaused;
    public GameObject PauseMenuObj;


    void Awake()
    {
        Resume();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        TogglePause();
    }


    public void Resume()
    {
        PauseMenuObj.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
    }
    void Pause()
    {
        PauseMenuObj.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
    }

    void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void RestartLevel()
    {
        //IDEA: At end of scene saves item info - at scene start it pulls those prefs
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

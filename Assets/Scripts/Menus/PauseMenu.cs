using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused;
    public GameObject PauseMenuObj;
    public GameObject settingMenu;
    PlayerControls playerControls;



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
        settingMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameIsPaused = false;
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        playerControls.lookSensitivity = PlayerPrefs.GetFloat(PlayerPrefsConstants.MOUSE_SENS);

    }
    void Pause()
    {
        PauseMenuObj.SetActive(true);
        settingMenu.SetActive(false);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }





    public void QuitGame()
    {
        Application.Quit();
    }
}

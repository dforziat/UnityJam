using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused;
    public GameObject PauseMenuObj;
    public GameObject settingMenu;
    PlayerControls playerControls;
    public GameObject GameOptions;
    public GameObject GraphicsOptions;
    public GameObject AudioOptions;
    public GameObject loadScreen;

    [Header("Controller Navigation")]
    public GameObject resumeButton;



    void Awake()
    {
        Resume();
        Cursor.lockState = CursorLockMode.Locked;

    }


    void Update()
    {
        if (PlayerControls.isDead || LevelManager.levelLoading)
        {
            return;
        }
        TogglePause();
    }


    public void Resume()
    {
        PauseMenuObj.SetActive(false);
        settingMenu.SetActive(false);
        GameOptions.SetActive(false);
        GraphicsOptions.SetActive(false);
        AudioOptions.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameIsPaused = false;
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        playerControls.lookSensitivity = PlayerPrefs.GetFloat(PlayerPrefsConstants.MOUSE_SENS);

    }
    void Pause()
    {
        PauseMenuObj.SetActive(true);
        settingMenu.SetActive(false);
        GameOptions.SetActive(false);
        GraphicsOptions.SetActive(false);
        AudioOptions.SetActive(false);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameIsPaused = true;
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    void TogglePause()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void RestartLevel()
    {
        Resume();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Instantiate(loadScreen, Vector3.zero, new Quaternion(0, 0, 0, 0));
    }


    public void closeSettings()
    {
        settingMenu.SetActive(false);
        PauseMenuObj.SetActive(true);

    }

    public void openSettings()
    {
        settingMenu.SetActive(true);
        PauseMenuObj.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}

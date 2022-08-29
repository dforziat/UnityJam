using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject settingMenuCanvas;
    public GameObject storyCanvas;
    public GameObject newGameConfirmCanvas;
    public GameObject levelSelectCanvas;
    public GameObject loadScreen;

    public GameObject title;
    public GameObject titleDropShadow;

    public GameObject challengeModePanel;
    public QuestionMark questionMark;

    GameObject lastSelectedButton;

    [Header("Controller Navigation")]
    public GameObject storyButton;
    public GameObject newGameButton;
    public GameObject graphicsButton;
    public GameObject levelSelectRightButton;
    public GameObject noButton;

    [Header("Audio")]
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        settingMenuCanvas.SetActive(false);
        loadScreen.SetActive(false);
        storyCanvas.SetActive(false);
        Time.timeScale = 1f;
        EventSystem.current.SetSelectedGameObject(storyButton);
        lastSelectedButton = storyButton;
        if (Input.GetJoystickNames().Length <= 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }


    void Update()
    {
        checkForChallengeModeController();

        checkForControllerReconnect();
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Level0");
        loadScreen.SetActive(true);
        gameObject.SetActive(false);
        SaveData.Instance.currentLevel = 1;
        SaveData.Instance.highestLevel = 1;

        audioSource.Play();
    }

    public void ContinueButton()
    {
        SceneManager.LoadSceneAsync(SaveData.Instance.currentLevel);
        loadScreen.SetActive(true);
        audioSource.Play();
    }

    public void DisplaySettings()
    {
        mainMenuCanvas.SetActive(false);
        settingMenuCanvas.SetActive(true);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(graphicsButton);

    }

    public void DisplayMainMenu()
    {
        mainMenuCanvas.SetActive(true);
        storyCanvas.SetActive(false);
        settingMenuCanvas.SetActive(false);
        levelSelectCanvas.SetActive(false);
        title.SetActive(true);
        titleDropShadow.SetActive(true);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(storyButton);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void challengeMode()
    {
        SceneManager.LoadSceneAsync(21);
        loadScreen.SetActive(true);
        gameObject.SetActive(false);
        audioSource.Play();
        checkForRogueManager();

    }

    public void DisplayStory()
    {
        storyCanvas.SetActive(true);
        audioSource.Play();
        mainMenuCanvas.SetActive(false);
        newGameConfirmCanvas.SetActive(false);
        EventSystem.current.SetSelectedGameObject(newGameButton);
    }

    public void DisplayNewGameConfirm()
    {
        audioSource.Play();
        storyCanvas.SetActive(false);
        newGameConfirmCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(noButton);
    }

    public void DisplayLevelSelect()
    {
        storyCanvas.SetActive(false);
        audioSource.Play();
        levelSelectCanvas.SetActive(true);
        title.SetActive(false);
        titleDropShadow.SetActive(false);
        EventSystem.current.SetSelectedGameObject(levelSelectRightButton);
    }

    public void checkForChallengeModeController()
    {
        if ((EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "ChallengeMode_Button") || questionMark.pointerEntered)
        {
            challengeModePanel.SetActive(true);
        }
        else
        {
            challengeModePanel.SetActive(false);
        }
    }

    public void checkForControllerReconnect()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            lastSelectedButton = EventSystem.current.currentSelectedGameObject;
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if(Input.GetAxisRaw("Horizontal") !=  0 || Input.GetAxisRaw("Vertical") != 0)
            {
                EventSystem.current.SetSelectedGameObject(lastSelectedButton);
            }
        }
    }

    private void checkForRogueManager()
    {
       GameObject rogueManager =  GameObject.Find("RogueManager");
        if(rogueManager != null)
        {
            Destroy(GameObject.Find("RogueManager"));
        }
    }
}

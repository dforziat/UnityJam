using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public GameObject settingMenuCanvas;

    [Header("Audio")]
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        settingMenuCanvas.SetActive(false);
    }


    void Update()
    {

    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetInt(PlayerPrefsConstants.CUR_LVL, 1);
        //Add in "Default Values" for player prefs, to esstentially delete saved data and start fresh
        PlayerPrefs.SetInt(PlayerPrefsConstants.GRAPPLEGUN, 0);
        PlayerPrefs.SetInt(PlayerPrefsConstants.SHOTGUN, 0);
        audioSource.Play();
    }

    public void ContinueButton()
    {
        //failsafe for how many levels we have
        if (PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL) < 1 || PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL) > 5)
        {
            PlayerPrefs.SetInt(PlayerPrefsConstants.CUR_LVL, 1);
            SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL));
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL));
        }
        audioSource.Play();
    }

    public void DisplaySettings()
    {
        mainMenuCanvas.enabled = false;
        settingMenuCanvas.SetActive(true);
        audioSource.Play();
    }

    public void DisplayMainMenu()
    {
        mainMenuCanvas.enabled = true;
        settingMenuCanvas.SetActive(false);
        audioSource.Play();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public GameObject settingMenuCanvas;



    void Start()
    {
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
            SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL));
    }

    public void DisplaySettings()
    {
        mainMenuCanvas.enabled = false;
        settingMenuCanvas.SetActive(true);
    }

    public void DisplayMainMenu()
    {
        mainMenuCanvas.enabled = true;
        settingMenuCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

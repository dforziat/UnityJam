using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas settingMenuCanvas;
    public Slider mouseSensSlider;
    public Text sensText;


    private void Awake()
    {
        mouseSensSlider.value = PlayerPrefs.GetFloat("mouseSens");
    }
    void Start()
    {
        sensText.text = mouseSensSlider.value.ToString();
        settingMenuCanvas.enabled = false;
        //Load Default PlayerPrefOptions
        if (PlayerPrefs.GetFloat("mouseSens") == 0)
        {
            PlayerPrefs.SetFloat("mouseSens", 1f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mouseSensSlider.enabled == true)
        {
            SetMouseSens(mouseSensSlider.value);
            sensText.text = mouseSensSlider.value.ToString();
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void DisplaySettings()
    {
        mainMenuCanvas.enabled = false;
        settingMenuCanvas.enabled = true;
    }

    public void DisplayMainMenu()
    {
        mainMenuCanvas.enabled = true;
        settingMenuCanvas.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMouseSens(float mouseSens)
    {
        PlayerPrefs.SetFloat("mouseSens", mouseSens);
    }
}

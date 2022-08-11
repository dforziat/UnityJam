using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class Settings : MonoBehaviour
{


    //This will be present in the main menu as well as the pause menu

    public AudioMixer audioMixer;

    Resolution[] resolutions;
    int resolutionIndex = 0;
    public TextMeshProUGUI resolutionText;
    private bool fullscreen;
    public TextMeshProUGUI fullScreenText;


    public Slider mouseSensSlider;
    public TextMeshProUGUI mouseSensText;

    [Header("Volume Sliders")]
    public Slider volumeSlider_Main;
    public Slider volumeSlider_SFX;
    public Slider volumeSlider_Music;
    [Header("Volume Slider Text")]
    public TextMeshProUGUI volText_Main;
    public TextMeshProUGUI volText_SFX;
    public TextMeshProUGUI volText_Music;


    //in the pause menu
    public GameObject settingMenu;
    public GameObject GameOptions;
    public GameObject GraphicsOptions;
    public GameObject AudioOptions;
    public GameObject creditsOptions;
    public GameObject controlsCanvas;


    private AudioSource audioSource;

    [Header("Controller Navigation")]
    public GameObject graphicsButton;
    public GameObject resolutionButtonRight;
    public GameObject mainVolumeSlider;
    public GameObject sensSlider;
    public GameObject creditsBackButton;
    public GameObject controlsRightButton;





    void Start()
    {
        LoadSettings();
        loadButtonResolutions();



        //FAILSAFE
        if (SaveData.Instance.mouseSens <= 0)
        {
            SaveData.Instance.mouseSens = 1f;
        }



        audioSource = GetComponent<AudioSource>();

    }

    public void SetVolume_Main(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume)*20);
        Debug.Log(volume);
        volText_Main.text =(Mathf.Round(volume * 100)).ToString();
        SaveData.Instance.masterVolume = volume;
    }

    public void SetVolume_SFX(float volume)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
        volText_SFX.text = (Mathf.Round(volume * 100)).ToString();
        SaveData.Instance.sfxVolume = volume;
    }

    public void SetVolume_Music(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        volText_Music.text = (Mathf.Round(volume * 100)).ToString();
        SaveData.Instance.musicVolume = volume;
    }

    public void SetSensitivity(float Sensitivity)
    {
        mouseSensText.text = (Mathf.Round(Sensitivity * 100)).ToString();
        SaveData.Instance.mouseSens = Sensitivity;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    

    void LoadSettings()
    {
        //Volume
        if(SaveData.Instance != null)
        {
            audioMixer.SetFloat("masterVolume", SaveData.Instance.masterVolume);
            volumeSlider_Main.value = SaveData.Instance.masterVolume;

            audioMixer.SetFloat("sfxVolume", SaveData.Instance.sfxVolume);
            volumeSlider_SFX.value = SaveData.Instance.sfxVolume;

            audioMixer.SetFloat("musicVolume", SaveData.Instance.musicVolume);
            volumeSlider_Music.value = SaveData.Instance.musicVolume;

            fullscreen = SaveData.Instance.fullScreen;
            //Resolution
            Screen.SetResolution(SaveData.Instance.resolutionWidth, SaveData.Instance.resolutionHeight, fullscreen, SaveData.Instance.frameRate);

            if (fullscreen)
            {
                fullScreenText.text = "Fullscreen: On";
            }
            else
            {
                fullScreenText.text = "Fullscreen: Off";
            }

            //Mouse Sensitivity
            mouseSensSlider.value = SaveData.Instance.mouseSens;
        }

    }

    public void closeSettings()
    {
        settingMenu.SetActive(false);
    }

    public void openSettings()
    {
        settingMenu.SetActive(true);
    }

    public void displayButton_Game()
    {
        GameOptions.SetActive(true);
        settingMenu.SetActive(false);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(sensSlider);
    }

    public void displayButton_Graphics()
    {
        GraphicsOptions.SetActive(true);
        settingMenu.SetActive(false);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(resolutionButtonRight);
    }

    public void displayButton_Audio()
    {
        AudioOptions.SetActive(true);
        settingMenu.SetActive(false);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(mainVolumeSlider);

    }

    public void displayButton_Credits()
    {
        creditsOptions.SetActive(true);
        GameOptions.SetActive(false);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(creditsBackButton);
    }
    public void backButton_Game()
    {
        GameOptions.SetActive(false);
        settingMenu.SetActive(true);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(graphicsButton);
    }

    public void displayControls()
    {

        GameOptions.SetActive(false);
        controlsCanvas.SetActive(true);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(controlsRightButton);
    }

    public void backButton_Graphics()
    {
        GraphicsOptions.SetActive(false);
        settingMenu.SetActive(true);
        audioSource.Play();
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, fullscreen, resolutions[resolutionIndex].refreshRate);
        SaveData.Instance.resolutionWidth = resolutions[resolutionIndex].width;
        SaveData.Instance.resolutionHeight = resolutions[resolutionIndex].height;
        SaveData.Instance.frameRate = resolutions[resolutionIndex].refreshRate;
        SaveData.Instance.fullScreen = fullscreen;

        EventSystem.current.SetSelectedGameObject(graphicsButton);
    }

    public void backButton_Audio()
    {
        AudioOptions.SetActive(false);
        settingMenu.SetActive(true);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(graphicsButton);
    }

    public void backButton_Credits()
    {
        creditsOptions.SetActive(false);
        GameOptions.SetActive(true);
        controlsCanvas.SetActive(false);
        audioSource.Play();
        EventSystem.current.SetSelectedGameObject(sensSlider);
    }

    private void loadButtonResolutions()
    {
        resolutions = Screen.resolutions;
        Resolution currentResolution = new Resolution();
        currentResolution.height = SaveData.Instance.resolutionHeight;
        currentResolution.width = SaveData.Instance.resolutionWidth;
        currentResolution.refreshRate = SaveData.Instance.frameRate;
        resolutionIndex = System.Array.IndexOf(resolutions, currentResolution);

       /* foreach (Resolution res in Screen.resolutions)
        {
            Debug.Log("Screen Resolutions: " + res.ToString());
        } */

        Debug.Log("Resolution Index: " + resolutionIndex);
        
        resolutionText.text = currentResolution.ToString();
    }


    public void resolutionRightButton()
    {
        if(resolutionIndex < resolutions.Length - 1)
        {
            audioSource.Play();
            resolutionIndex++;
            resolutionText.text = resolutions[resolutionIndex].ToString();
        }

    }

    public void resolutionLeftButton()
    {
        if (resolutionIndex > 0)
        {
            audioSource.Play();
            resolutionIndex--;
            resolutionText.text = resolutions[resolutionIndex].ToString();
        }
    }


    public void fullScreenToggle()
    {
        audioSource.Play();
        if (fullscreen)
        {
            fullscreen = false;
            fullScreenText.text = "Fullscreen: Off";
        }
        else if (!fullscreen)
        {
            fullscreen = true;
            fullScreenText.text = "Fullscreen: On";
        }
    }
}

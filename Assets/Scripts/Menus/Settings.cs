using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Linq;



public class Settings : MonoBehaviour
{


    //This will be present in the main menu as well as the pause menu

    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    List<Resolution> finalRes = new List<Resolution>();

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







    void Start()
    {

        LoadResolutions();
        LoadSettings();

        //FAILSAFE
        if (PlayerPrefs.GetFloat(PlayerPrefsConstants.MOUSE_SENS) == 0)
        {
            PlayerPrefs.SetFloat(PlayerPrefsConstants.MOUSE_SENS, 1f);
        }



    }

    public void SetVolume_Main(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume)*20);
        volText_Main.text = "Main - "+(Mathf.Round(volume * 100))+"%";
        PlayerPrefs.SetFloat(PlayerPrefsConstants.MASTER_VOLUME, volume);
    }

    public void SetVolume_SFX(float volume)
    {
        audioMixer.SetFloat("sfxVolume", Mathf.Log10(volume) * 20);
        volText_SFX.text = "SFX - " + (Mathf.Round(volume * 100)) + "%";
        PlayerPrefs.SetFloat(PlayerPrefsConstants.SFX_VOLUME, volume);
    }

    public void SetVolume_Music(float volume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        volText_Music.text = "Music - " + (Mathf.Round(volume * 100)) + "%";
        PlayerPrefs.SetFloat(PlayerPrefsConstants.MUSIC_VOLUME, volume);
    }

    public void SetSensitivity(float Sensitivity)
    {
        mouseSensText.text = "Sensitivity: " + (Mathf.Round(Sensitivity * 100));
        PlayerPrefs.SetFloat(PlayerPrefsConstants.MOUSE_SENS, Sensitivity);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        
        Resolution resolution = finalRes[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt(PlayerPrefsConstants.RESOLUTION_WIDTH, resolution.width);
        PlayerPrefs.SetInt(PlayerPrefsConstants.RESOLUTION_HEIGHT, resolution.height);
    }

    void LoadResolutions()
    {
        float nativeWidth = Screen.currentResolution.width;
        float nativeHeight = Screen.currentResolution.height;
        float nativeRefreshRate = Screen.currentResolution.refreshRate;

        //Only 60,120,144 Hz displayed
        //resolutions = Screen.resolutions.Where(resolution => (resolution.refreshRate == nativeRefreshRate)).ToArray();
        resolutions = Screen.resolutions;


        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {

            float parseWidth = resolutions[i].width;
            float parseHeight = resolutions[i].height;


            string option = resolutions[i].width + " x " + resolutions[i].height +" (" +resolutions[i].refreshRate + "HZ)";



            if ((parseWidth / parseHeight) == (nativeWidth / nativeHeight))
            {
                options.Add(option);
                finalRes.Add(resolutions[i]);
                Debug.Log(parseWidth +" x "+ parseHeight);
            }

        }

        for (int i = 0; i < finalRes.Count; i++)
        {
            if (finalRes[i].width == Screen.currentResolution.width &&
                finalRes[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void LoadSettings()
    {
        //Volume
        audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat(PlayerPrefsConstants.MASTER_VOLUME));
        volumeSlider_Main.value = PlayerPrefs.GetFloat(PlayerPrefsConstants.MASTER_VOLUME);

        audioMixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat(PlayerPrefsConstants.SFX_VOLUME));
        volumeSlider_SFX.value = PlayerPrefs.GetFloat(PlayerPrefsConstants.SFX_VOLUME);

        audioMixer.SetFloat("musicVolume", PlayerPrefs.GetFloat(PlayerPrefsConstants.MUSIC_VOLUME));
        volumeSlider_Music.value = PlayerPrefs.GetFloat(PlayerPrefsConstants.MUSIC_VOLUME);

        //Resolution
        Screen.SetResolution(PlayerPrefs.GetInt(PlayerPrefsConstants.RESOLUTION_WIDTH), PlayerPrefs.GetInt(PlayerPrefsConstants.RESOLUTION_HEIGHT), true);
       

        //Mouse Sensitivity
        mouseSensSlider.value = PlayerPrefs.GetFloat(PlayerPrefsConstants.MOUSE_SENS);


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
    }

    public void displayButton_Graphics()
    {
        GraphicsOptions.SetActive(true);
        settingMenu.SetActive(false);
    }

    public void displayButton_Audio()
    {
        AudioOptions.SetActive(true);
        settingMenu.SetActive(false);
    }
    public void backButton_Game()
    {
        GameOptions.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void backButton_Graphics()
    {
        GraphicsOptions.SetActive(false);
        settingMenu.SetActive(true);
    }

    public void backButton_Audio()
    {
        AudioOptions.SetActive(false);
        settingMenu.SetActive(true);
    }

}

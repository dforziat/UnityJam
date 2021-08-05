using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{


    //This will be present in the main menu as well as the pause menu

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public Slider mouseSensSlider;
    public Slider volumeSlider;

    //in the pause menu
    public GameObject settingMenu;



    private void Start()
    {
        LoadResolutions();
        LoadSettings();


        //FAILSAFE
        if (PlayerPrefs.GetFloat(PlayerPrefsConstants.MOUSE_SENS) == 0)
        {
            PlayerPrefs.SetFloat(PlayerPrefsConstants.MOUSE_SENS, 1f);
        }
        
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
        PlayerPrefs.SetFloat(PlayerPrefsConstants.MASTER_VOLUME, volume);
    }

    public void SetSensitivity(float Sensitivity)
    {
        PlayerPrefs.SetFloat(PlayerPrefsConstants.MOUSE_SENS, Sensitivity);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt(PlayerPrefsConstants.RESOLUTION_WIDTH, resolution.width);
        PlayerPrefs.SetInt(PlayerPrefsConstants.RESOLUTION_HEIGHT, resolution.height);
    }

    void LoadResolutions()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
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
        //Master Volume
        audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat(PlayerPrefsConstants.MASTER_VOLUME));
        volumeSlider.value = PlayerPrefs.GetFloat(PlayerPrefsConstants.MASTER_VOLUME);

        //Resolution
        Screen.SetResolution(PlayerPrefs.GetInt(PlayerPrefsConstants.RESOLUTION_WIDTH), PlayerPrefs.GetInt(PlayerPrefsConstants.RESOLUTION_HEIGHT), Screen.fullScreen);

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

}

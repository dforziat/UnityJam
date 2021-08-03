using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{


    //This will be present in the main menu as well as the pause menu
    //This is will save and apply, apply method should be public so it can fire in the main menu at start from prefs

    //Mouse Sensativity

    //Resolution

    //Volume

    public AudioMixer audioMixer;
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;

    private void Start()
    {
        LoadResolutions();
        LoadSettings();


    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);

        PlayerPrefs.SetFloat(PlayerPrefsConstants.MASTER_VOLUME, volume);



    }

    public void SetSensitivity(float Sensitivity)
    {

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


        //Resolution
        Screen.SetResolution(PlayerPrefs.GetInt(PlayerPrefsConstants.RESOLUTION_WIDTH), PlayerPrefs.GetInt(PlayerPrefsConstants.RESOLUTION_HEIGHT), Screen.fullScreen);

        //Mouse Sensitivity



    }


}

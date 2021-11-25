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
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    List<Resolution> finalRes = new List<Resolution>();

    public Slider mouseSensSlider;

    public TextMeshProUGUI volText;
    public Slider volumeSlider;

    //in the pause menu
    public GameObject settingMenu;







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

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", Mathf.Log10(volume)*20);
        volText.text = "Volume - "+(Mathf.Round(volume * 100))+"%";
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
        
        Resolution resolution = finalRes[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt(PlayerPrefsConstants.RESOLUTION_WIDTH, resolution.width);
        PlayerPrefs.SetInt(PlayerPrefsConstants.RESOLUTION_HEIGHT, resolution.height);
    }

    void LoadResolutions()
    {


        //Only 60,120,144 Hz displayed
        resolutions = Screen.resolutions.Where(resolution => (resolution.refreshRate == 60 || resolution.refreshRate == 144 || resolution.refreshRate == 120)).ToArray();


        float nativeWidth = Screen.currentResolution.width;
        float nativeHeight = Screen.currentResolution.height;



        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {

            float parseWidth = resolutions[i].width;
            float parseHeight = resolutions[i].height;


            string option = resolutions[i].width + " x " + resolutions[i].height +" - " +resolutions[i].refreshRate + " Hz";



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
        //Master Volume
        audioMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat(PlayerPrefsConstants.MASTER_VOLUME));
        volumeSlider.value = PlayerPrefs.GetFloat(PlayerPrefsConstants.MASTER_VOLUME);

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

}

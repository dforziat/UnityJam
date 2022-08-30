using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Steamworks;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    SaveManager SaveManager;
    private GameObject levelCompleteScreen;
    private GameObject nextLevelButton;
    public static bool levelLoading;

    private TextMeshProUGUI finaltimeText;
    private TextMeshProUGUI besttimeText;
    private TextMeshProUGUI devtimeText;



    int mins;
    int secs;
    int miliSecs;
    float currentTime;

    string timeFormat = "{0,2:00}:{1,2:00}:{2,2:00}";

    float curBesttime;
    int bestMins;
    int bestSecs;
    int bestMiliSecs;

    float devTime;


    void Start()
    {
        levelCompleteScreen = GameObject.Find("LevelCompleteCanvas");
        finaltimeText = levelCompleteScreen.gameObject.transform.Find("playerTime").GetComponent<TextMeshProUGUI>();
        besttimeText = levelCompleteScreen.gameObject.transform.Find("bestTime").GetComponent<TextMeshProUGUI>();
        devtimeText = levelCompleteScreen.gameObject.transform.Find("devTime").GetComponent<TextMeshProUGUI>();
        nextLevelButton = levelCompleteScreen.gameObject.transform.Find("Next Level Button").gameObject;
        levelLoading = false;
        levelCompleteScreen.SetActive(false);
        Time.timeScale = 1;
        SaveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        levelLoading = true;
        levelCompleteScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(nextLevelButton);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        timer();
        displayDevTime();
        checkBest();
        displayBest();
        SaveData.Instance.currentLevel++;
        SaveManager.SavePrefs();
        checkDevTimeAchievement();
        GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<WeaponSwitching>().lockWeaponSwitch = true;
    }



    public void timer()
    {
        mins = (int)(Time.timeSinceLevelLoad / 60);
        secs = (int)(Time.timeSinceLevelLoad % 60);
        miliSecs = Mathf.RoundToInt((Time.timeSinceLevelLoad - mins * 60 - secs) * 100);
        currentTime = Time.timeSinceLevelLoad;

        finaltimeText.text = string.Format(timeFormat, mins, secs, miliSecs);
    }


    public void checkBest()
    {
        curBesttime = SaveData.Instance.bestTime[SaveData.Instance.currentLevel];

        if (currentTime < curBesttime || curBesttime == 0)
        {
            SaveData.Instance.bestTime[SaveData.Instance.currentLevel] = currentTime;
        }
    }

    public void displayBest()
    {
        curBesttime = SaveData.Instance.bestTime[SaveData.Instance.currentLevel];
        bestMins = (int)(curBesttime / 60);
        bestSecs = (int)(curBesttime % 60);
        bestMiliSecs = Mathf.RoundToInt((curBesttime - bestMins * 60 - bestSecs) * 100);
        besttimeText.text = string.Format(timeFormat, bestMins, bestSecs, bestMiliSecs);
    }

    public void displayDevTime()
    {
        devTime = DevTimes.devTimesList[SaveData.Instance.currentLevel];
        float bestMins = (int)(devTime / 60);
        float bestSecs = (int)(devTime % 60);
        float bestMiliSecs = Mathf.RoundToInt((devTime - bestMins * 60 - bestSecs) * 100);
        devtimeText.text = string.Format(timeFormat, bestMins, bestSecs, bestMiliSecs);
    }


    public void Level_0_Complete()
    {

        //Loads Level 1 Everytime
        
        Debug.Log("Level Advanced");
        SaveData.Instance.currentLevel = 2;
        SaveManager.SavePrefs();
        SceneManager.LoadScene("Level1");
    }

    public void checkDevTimeAchievement()
    {
        if (currentTime < devTime)
        {
            if (SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement(SteamAchievementConstants.DEV_TIME, out bool achievementUnlocked);
                if (!achievementUnlocked)
                {
                    SteamScript.incrementPlatStat();
                    SteamUserStats.SetAchievement(SteamAchievementConstants.DEV_TIME);
                    SteamUserStats.StoreStats();
                }

            }
        }
       
    }

}



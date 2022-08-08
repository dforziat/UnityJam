using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    SaveManager SaveManager;
    PlayerControls playerControls;
    private GameObject levelCompleteScreen;
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



    void Start()
    {
        levelCompleteScreen = GameObject.Find("LevelCompleteCanvas");
        finaltimeText = levelCompleteScreen.gameObject.transform.Find("playerTime").GetComponent<TextMeshProUGUI>();
        besttimeText = levelCompleteScreen.gameObject.transform.Find("bestTime").GetComponent<TextMeshProUGUI>();
        devtimeText = levelCompleteScreen.gameObject.transform.Find("devTime").GetComponent<TextMeshProUGUI>();
        levelLoading = false;
        levelCompleteScreen.SetActive(false);
        Time.timeScale = 1;
        SaveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        levelLoading = true;
        levelCompleteScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        timer();
        displayDevTime();
        checkBest();
        displayBest();
        SaveData.Instance.currentLevel++;
        SaveManager.SavePrefs();
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
        float devTime = DevTimes.devTimesList[SaveData.Instance.currentLevel];
        float bestMins = (int)(devTime / 60);
        float bestSecs = (int)(devTime % 60);
        float bestMiliSecs = Mathf.RoundToInt((devTime - bestMins * 60 - bestSecs) * 100);
        devtimeText.text = string.Format(timeFormat, bestMins, bestSecs, bestMiliSecs);
    }


    public void Level_0_Complete()
    {

        //Uncomment these once level is solid




        //Loads Level 1 Everytime
        
        Debug.Log("Level Advanced");
        SaveData.Instance.currentLevel = 2;
        SaveManager.SavePrefs();
        SceneManager.LoadScene("Level1");
    }

}



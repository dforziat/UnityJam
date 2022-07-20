using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    SaveManager SaveManager;
    PlayerControls playerControls;
    public GameObject levelCompleteScreen;
    public static bool levelLoading;

    public TextMeshProUGUI finaltimeText;
    public TextMeshProUGUI besttimeText;

    public GameObject loadScreen;

    int mins;
    int secs;
    int miliSecs;
    float currentTime;



    string timeFormat = "{0,2:00}:{1,2:00}:{2,2:00}";



    float curBesttime;
    int bestMins;
    int bestSecs;
    int bestMiliSecs;
    public bool poop;



    void Start()
    {
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
        checkBest();
        displayBest();
        playerControls.curLevel++;//This is a bug
        SaveManager.SavePrefs();

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL));
        Instantiate(loadScreen, Vector3.zero, new Quaternion(0,0,0,0));
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
        curBesttime = PlayerPrefs.GetFloat(PlayerPrefsConstants.BEST_TIME + playerControls.curLevel);

        if (currentTime < curBesttime || curBesttime == 0)
        {
            PlayerPrefs.SetFloat(PlayerPrefsConstants.BEST_TIME + playerControls.curLevel, currentTime);
        }
    }

    public void displayBest()
    {
        curBesttime = PlayerPrefs.GetFloat(PlayerPrefsConstants.BEST_TIME + playerControls.curLevel);

        bestMins = (int)(curBesttime / 60);
        bestSecs = (int)(curBesttime % 60);
        bestMiliSecs = Mathf.RoundToInt((curBesttime - bestMins * 60 - bestSecs) * 100);

        besttimeText.text = string.Format(timeFormat, bestMins, bestSecs, bestMiliSecs);
    }

    public void Level_0_Complete()
    {

        //Uncomment these once level is solid
        //playerControls.curLevel = 1;
        //SaveManager.SavePrefs();


        //Loads Level 1 Everytime

        Debug.Log("Level Advanced");
        SceneManager.LoadScene("Level1");
    }

}



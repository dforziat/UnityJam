using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    SaveManager SaveManager;
    PlayerControls playerControls;
    public Canvas levelCompleteCanvas;
    public static bool levelLoading;

    public TextMeshProUGUI finaltimeText;
    int mins;
    int secs;
    int miliSecs;
    int currentTime;
    string timeFormat = "{0,2:00}:{1,2:00}:{2,2:00}";




    void Start()
    {
        levelLoading = false;
        levelCompleteCanvas.enabled = false;
        Time.timeScale = 1;
        SaveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        testBest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        levelLoading = true;
        levelCompleteCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerControls.curLevel++;
        SaveManager.SavePrefs();


    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL));
    }

    public void timer()
    {
        mins = (int)(Time.timeSinceLevelLoad / 60);

        secs = (int)(Time.timeSinceLevelLoad % 60);

        miliSecs = Mathf.RoundToInt((Time.timeSinceLevelLoad - mins * 60 - secs) * 100);

        //finaltimeText.text = mins + ":" + secs + ":" + miliSecs;

        currentTime = mins + secs + miliSecs;
        
        //timeCheck()
      

  
        finaltimeText.text = string.Format(timeFormat, mins, secs, miliSecs);
        //best-time-text.text = String version of saved best time
    }

    //method to load all best times loadTimes()
    //method, for loop, if curLvl = i then check time?

    public void testBest()
    {
        int curLvl = PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL);
        int curBesttime = PlayerPrefs.GetInt(PlayerPrefsConstants.BEST_TIME + curLvl);
        Debug.Log(PlayerPrefsConstants.BEST_TIME + curLvl);
    }




   // public void find_curLvl_bestTime()
   // {
       // int curLvl = PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL);

       // for (int i=1; i < SceneManager.sceneCount; i++)
      //  {
        //    if (curLvl==i)
     //       {
               // int curBesttime = PlayerPrefs.GetInt(PlayerPrefsConstants.BEST_TIME_+i);
      //     }
      //  }
    //}
}



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
    string timeFormat = "{0,2:00}:{1,2:00}:{2,2:00}";




    void Start()
    {
        levelLoading = false;
        levelCompleteCanvas.enabled = false;
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
        levelCompleteCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        playerControls.curLevel++;
        SaveManager.SavePrefs();
        timer();

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

        finaltimeText.text = string.Format(timeFormat, mins, secs, miliSecs);
    }
}



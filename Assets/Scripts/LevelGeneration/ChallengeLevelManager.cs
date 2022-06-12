using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengeLevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject levelCompleteCanvas;
    TextMeshProUGUI levelTimeText;
    TextMeshProUGUI totalTimeText;

    string timeFormat = "{0,2:00}:{1,2:00}:{2,2:00}";
    float levelTime = 0;

    void Start()
    {
        levelCompleteCanvas = GameObject.Find("ChallengeLevelCompleteCanvas");
        levelCompleteCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void levelComplete()
    {
        RougeManager rm = GameObject.FindObjectOfType<RougeManager>();
        rm.numLevelsCompleted++;
        //display the GUI
        levelCompleteCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Set Level time on GUI
        int mins = (int)(Time.timeSinceLevelLoad / 60);
        int secs = (int)(Time.timeSinceLevelLoad % 60);
        int miliSecs = Mathf.RoundToInt((Time.timeSinceLevelLoad - mins * 60 - secs) * 100);
        levelTimeText = levelCompleteCanvas.transform.Find("playerTime").GetComponent<TextMeshProUGUI>();
        levelTimeText.text = string.Format(timeFormat, mins, secs, miliSecs);

        //Set Total Run Time
        levelTime = Time.timeSinceLevelLoad;
        rm.totalTime += levelTime;

        mins = (int)(rm.totalTime / 60);
        secs = (int)(rm.totalTime % 60);
        miliSecs = Mathf.RoundToInt((rm.totalTime - mins * 60 - secs) * 100);
        totalTimeText = levelCompleteCanvas.transform.Find("totalTime").GetComponent<TextMeshProUGUI>();
        totalTimeText.text = string.Format(timeFormat, mins, secs, miliSecs);

        if (rm.numLevelsCompleted < 5)
        {
            GameObject runCompleteText = levelCompleteCanvas.transform.Find("RunCompleteText").gameObject;
            runCompleteText.SetActive(false);

        }
        else
        {
            GameObject nextLevelButton = levelCompleteCanvas.transform.Find("Next Level Button").gameObject;
            nextLevelButton.SetActive(false);

            GameObject levelCompleteText = levelCompleteCanvas.transform.Find("LevelComplete").gameObject;
            levelCompleteText.SetActive(false);
        }
    }
}

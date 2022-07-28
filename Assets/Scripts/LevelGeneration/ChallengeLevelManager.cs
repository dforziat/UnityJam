using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChallengeLevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject levelCompleteCanvas;
    GameObject storyLevelCompleteCanvas;
    TextMeshProUGUI levelTimeText;
    TextMeshProUGUI totalTimeText;
    RougeManager rm;

    string timeFormat = "{0,2:00}:{1,2:00}:{2,2:00}";
    float levelTime = 0;

    void Start()
    {
        rm = GameObject.FindObjectOfType<RougeManager>();
        levelCompleteCanvas = GameObject.Find("ChallengeLevelCompleteCanvas");
        storyLevelCompleteCanvas = GameObject.Find("LevelCompleteCanvas");
        storyLevelCompleteCanvas.SetActive(false);
        levelCompleteCanvas.SetActive(false);
        PauseMenu.GameIsPaused = false;

        //setup player guns
        loadUnlockedWeapons();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void levelComplete()
    {
        rm.numLevelsCompleted++;
        //display the GUI
        levelCompleteCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenu.GameIsPaused = true;

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

            GameObject bestTimeText = levelCompleteCanvas.transform.Find("BestTimeText").gameObject;
            bestTimeText.SetActive(false);

            GameObject bestTimeNum = levelCompleteCanvas.transform.Find("bestTime").gameObject;
            bestTimeNum.SetActive(false);

        }
        else
        {
            float challengeBestTime = PlayerPrefs.GetFloat(PlayerPrefsConstants.CHALLENGE_BEST_TIME, 0);
            if (rm.totalTime < challengeBestTime || challengeBestTime == 0)
            {
                PlayerPrefs.SetFloat(PlayerPrefsConstants.CHALLENGE_BEST_TIME, rm.totalTime);
            }

            GameObject nextLevelButton = levelCompleteCanvas.transform.Find("Next Level Button").gameObject;
            nextLevelButton.SetActive(false);

            GameObject levelCompleteText = levelCompleteCanvas.transform.Find("LevelComplete").gameObject;
            levelCompleteText.SetActive(false);

        }

        saveUnlockedWeapons();
    }

    public void loadUnlockedWeapons()
    {
        GameObject weaponsHud = GameObject.FindGameObjectWithTag("WeaponsHud");
     
        foreach (string weaponName in rm.unlockedWeaponList)
        {
            weaponsHud.transform.Find(weaponName).tag = "unlocked";
        }

    }

    public void saveUnlockedWeapons()
    {
        GameObject weaponsHud = GameObject.FindGameObjectWithTag("WeaponsHud");
        rm.unlockedWeaponList.Clear();
        foreach(Transform weapon in weaponsHud.transform)
        {
            if(weapon.tag == "unlocked")
            {
                rm.unlockedWeaponList.Add(weapon.name);
            }
        }
    }
}

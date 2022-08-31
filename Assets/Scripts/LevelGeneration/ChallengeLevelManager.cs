using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChallengeLevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject levelCompleteCanvas;
    GameObject storyLevelCompleteCanvas;
    TextMeshProUGUI levelTimeText;
    TextMeshProUGUI totalTimeText;
    // RougeManager rm;
    public GameObject loadScreen;


    string timeFormat = "{0,2:00}:{1,2:00}:{2,2:00}";
    float levelTime = 0;

    void Start()
    {
        //rm = GameObject.FindObjectOfType<RougeManager>();
        levelCompleteCanvas = GameObject.Find("ChallengeLevelCompleteCanvas");
        storyLevelCompleteCanvas = GameObject.Find("LevelCompleteCanvas");
        storyLevelCompleteCanvas.SetActive(false);
        levelCompleteCanvas.SetActive(false);
        PauseMenu.GameIsPaused = false;
        Time.timeScale = 1;
        LevelManager.levelLoading = false;
        //setup player guns
        loadUnlockedWeapons();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void levelComplete()
    {
        RougeManager.instance.numLevelsCompleted++;
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
        RougeManager.instance.totalTime += levelTime;

        mins = (int)(RougeManager.instance.totalTime / 60);
        secs = (int)(RougeManager.instance.totalTime % 60);
        miliSecs = Mathf.RoundToInt((RougeManager.instance.totalTime - mins * 60 - secs) * 100);
        totalTimeText = levelCompleteCanvas.transform.Find("totalTime").GetComponent<TextMeshProUGUI>();
        totalTimeText.text = string.Format(timeFormat, mins, secs, miliSecs);

        if (RougeManager.instance.numLevelsCompleted < 5)
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
            float challengeBestTime = SaveData.Instance.challengeBestTime;
            if (RougeManager.instance.totalTime < challengeBestTime || challengeBestTime <= 0f)
            {
                SaveData.Instance.challengeBestTime = RougeManager.instance.totalTime;
                challengeBestTime = RougeManager.instance.totalTime;
            }

            GameObject bestTimeNum = levelCompleteCanvas.transform.Find("bestTime").gameObject;
           int bestmins = (int)(RougeManager.instance.totalTime / 60);
           int  bestsecs = (int)(RougeManager.instance.totalTime % 60);
           int bestmiliSecs = Mathf.RoundToInt((challengeBestTime - bestmins * 60 - bestsecs) * 100);
            bestTimeNum.GetComponent<TextMeshProUGUI>().text = string.Format(timeFormat, bestmins, bestsecs, bestmiliSecs);

            GameObject nextLevelButton = levelCompleteCanvas.transform.Find("Next Level Button").gameObject;
            nextLevelButton.SetActive(false);

            GameObject levelCompleteText = levelCompleteCanvas.transform.Find("LevelComplete").gameObject;
            levelCompleteText.SetActive(false);

            if (SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement(SteamAchievementConstants.CHALLENGE_MODE, out bool achievementUnlocked);
                if (!achievementUnlocked)
                {
                    SteamScript.incrementPlatStat();
                    SteamUserStats.SetAchievement(SteamAchievementConstants.CHALLENGE_MODE);
                    SteamUserStats.StoreStats();
                }

            }

        }

        saveUnlockedWeapons();
    }

    public void loadUnlockedWeapons()
    {
        GameObject weaponsHud = GameObject.FindGameObjectWithTag("WeaponsHud");
     
        foreach (string weaponName in RougeManager.instance.unlockedWeaponList)
        {
            weaponsHud.transform.Find(weaponName).tag = "unlocked";
        }

    }

    public void saveUnlockedWeapons()
    {
        GameObject weaponsHud = GameObject.FindGameObjectWithTag("WeaponsHud");
        RougeManager.instance.unlockedWeaponList.Clear();
        foreach(Transform weapon in weaponsHud.transform)
        {
            if(weapon.tag == "unlocked")
            {
                RougeManager.instance.unlockedWeaponList.Add(weapon.name);
            }
        }
    }

    public void nextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        loadScreen.SetActive(true);
    }
}

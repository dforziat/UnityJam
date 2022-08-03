using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    // Start is called before the first frame update
    public int levelNum;

    public TextMeshProUGUI levelTitle;
    public TextMeshProUGUI bestTimeNum;

    string timeFormat = "{0,2:00}:{1,2:00}:{2,2:00}";

    public GameObject loadScreen;

    private Button button;
    void Start()
    {
        levelTitle.text = levelTitle.text + levelNum;
        displayBest();
        button = GetComponent<Button>();

        if(levelNum > SaveData.Instance.highestLevel){
            button.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayBest()
    {
        float curBesttime = SaveData.Instance.bestTime[levelNum];


        float bestMins = (int)(curBesttime / 60);
        float bestSecs = (int)(curBesttime % 60);
        float bestMiliSecs = Mathf.RoundToInt((curBesttime - bestMins * 60 - bestSecs) * 100);

        bestTimeNum.text = string.Format(timeFormat, bestMins, bestSecs, bestMiliSecs);
    }

    public void loadLevel()
    {
        SaveData.Instance.currentLevel = levelNum;
        SceneManager.LoadSceneAsync(levelNum);
        Instantiate(loadScreen, Vector3.zero, new Quaternion(0, 0, 0, 0));
    }

}

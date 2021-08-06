using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    SaveManager SaveManager;
    PlayerControls playerControls;
    public Canvas levelCompleteCanvas;
    public static bool levelLoading;



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

}

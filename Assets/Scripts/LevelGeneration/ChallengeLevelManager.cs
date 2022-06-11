using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeLevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject levelCompleteCanvas;

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
        if (rm.numLevelsCompleted < 5)
        {
            //display the GUI
            levelCompleteCanvas.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }
    }
}

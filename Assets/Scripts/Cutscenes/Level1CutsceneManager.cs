using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level1CutsceneManager : MonoBehaviour
{
 
    public GameObject GUI;
    public GameObject Dialogue;
    PlayerControls playerControls;



    // Start is called before the first frame update
    void Start()
    {
        //player starts with no HUD, timeline locks them in place
        GUI.SetActive(false);
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        //enables hud, scene ends
        
        Debug.Log("Level Advanced");
        playerControls.curLevel = 2;
        SaveData.Instance.currentLevel = playerControls.curLevel;
        SceneManager.LoadScene("Level1");
        GUI.SetActive(true);

    }


}

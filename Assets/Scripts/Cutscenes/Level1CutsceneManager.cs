using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Level1CutsceneManager : MonoBehaviour
{
 
    public GameObject GUI;
    public GameObject Dialogue;
    public GameObject loadScreen;



    // Start is called before the first frame update
    void Start()
    {
        //player starts with no HUD, timeline locks them in place
        GUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel()
    {
        //enables hud, scene ends
        
        Debug.Log("Level Advanced");
        SaveData.Instance.currentLevel = 2;
        SceneManager.LoadSceneAsync("Level1");
        Instantiate(loadScreen, Vector3.zero, new Quaternion(0, 0, 0, 0));

    }


}

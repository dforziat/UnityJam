using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Level1CutsceneManager : MonoBehaviour
{
 
    public GameObject GUI;
    public GameObject Dialogue;




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
        GUI.SetActive(true);

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    SaveManager SaveManager;
    PlayerControls playerControls;

    //How to unlock weapon
    //public Transform shotgun;
    //private GameObject weaponsHud;



    void Start()
    {
        SaveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();

        //How to unlock weapon
        //weaponsHud = GameObject.FindGameObjectWithTag("WeaponsHud");
      //  shotgun.tag = "unlocked";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        playerControls.curLevel++;
        SaveManager.SetPrefs();

    }


}

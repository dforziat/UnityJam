using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    SaveManager SaveManager;
    PlayerControls playerControls;





    void Start()
    {
        SaveManager = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SaveManager>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        playerControls.curLevel++;
        SaveManager.SavePrefs();

    }


}

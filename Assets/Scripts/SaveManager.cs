using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    PlayerControls playerControls;

    // Start is called before the first frame update
    void Start()
    {
        SetPrefs();

    }

    // Update is called once per frame
    void Update()
    {
        //LoadPref();
    }

    public void SetPrefs()
    {
        //Happens at End of Level, pushes current loadout to preferences

        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();


        //HandgunAmmo
        PlayerPrefs.SetInt("handgunAmmo", playerControls.handgunAmmo);
        PlayerPrefs.SetInt("handgunAmmo", 99);
        


        //ShotgunAmmo;
        PlayerPrefs.SetInt("shotgunAmmo", playerControls.shotgunAmmo);
        

        //Current HP
        PlayerPrefs.SetInt("curHp", playerControls.curHp);

        //Set curLevel
        //TODO: Change LevelComplete Script to LevelManager
        PlayerPrefs.SetInt("curLevel", playerControls.curLevel);

        //Save
        PlayerPrefs.Save();
        

    }

    void LoadPref()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SetPrefs();
            PlayerPrefs.SetInt("handgunAmmo", 99);
            playerControls.handgunAmmo = PlayerPrefs.GetInt("handgunAmmo");
            playerControls.shotgunAmmo = PlayerPrefs.GetInt("shotgunAmmo");
            playerControls.curHp = PlayerPrefs.GetInt("curHP");
            playerControls.curLevel = PlayerPrefs.GetInt("curLevel");
            Debug.Log("Loaded PREFS");

        }

    }
}

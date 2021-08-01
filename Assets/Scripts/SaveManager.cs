using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    int prefHandgunAmmo;
    int prefShotgunAmmo;
    int prefCurHP;
    PlayerControls playerControls;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPrefs()
    {
        //Happens at End of Level, pushes current loadout to preferences

        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();


        //HandgunAmmo
        PlayerPrefs.SetInt("handgunAmmo", playerControls.handgunAmmo);

        //ShotgunAmmo;
        PlayerPrefs.SetInt("shotgunAmmo", playerControls.shotgunAmmo);

        //Current HP
        PlayerPrefs.SetInt("curHp", playerControls.curHp);

        //Set curLevel
        //TODO: Change LevelComplete Script to LevelManager
        //PlayerPrefs.SetInt("curLevel", playerControls.curLevel);

        //Save
        PlayerPrefs.Save();
    }


}

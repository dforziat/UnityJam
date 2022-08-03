using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    PlayerControls playerControls;
    WeaponSwitching weaponSwitching;

    //How to unlock weapon
    public Transform shotgun;
    public Transform grapplegun;
    public Transform machinegun;
    public Transform richochetgun;
    public Transform spear;
    private GameObject weaponsHud;


    // Start is called before the first frame update
    void Start()
    {
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        weaponsHud = GameObject.FindGameObjectWithTag("WeaponsHud");
        weaponSwitching = GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<WeaponSwitching>();
        LoadPref();
    }

    // Update is called once per frame
    void Update()
    {
        //Press H to reset player prefs to a default
        //DebugPref();
        unlockAllWeapons();
    }

    public void SavePrefs()
    {
        //Happens at End of Level, pushes current loadout to preferences

        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        weaponsHud = GameObject.FindGameObjectWithTag("WeaponsHud");
        weaponSwitching = GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<WeaponSwitching>();

        //Set curLevel
        SaveData.Instance.currentLevel = playerControls.curLevel;

        //Set Max Level
        int highestLevel = SaveData.Instance.highestLevel;
        int curLevel = SaveData.Instance.currentLevel;
        if (highestLevel < curLevel)
        {
            SaveData.Instance.highestLevel = curLevel;
        }

        //Set Last Weapon
        SaveData.Instance.curWeapon = weaponSwitching.curWeapon;


        //Set Weapons
        if (shotgun.tag == "unlocked")
            SaveData.Instance.shotgun = true;

        if (grapplegun.tag == "unlocked")
            SaveData.Instance.grapplegun = true;

        if (richochetgun.tag == "unlocked")
            SaveData.Instance.richochetgun = true;

        if (machinegun.tag == "unlocked")
            SaveData.Instance.machinegun = true;

        if (spear.tag == "unlocked")
            SaveData.Instance.spear = true;

        //Save
        PlayerPrefs.Save();
        

    }

    public void LoadPref()
    {

        if(SceneManager.GetActiveScene().name == "RandomLevel")
        {
            return;
        }

        playerControls.curLevel = SaveData.Instance.currentLevel;
        weaponSwitching.curWeapon = SaveData.Instance.curWeapon;

        if (SaveData.Instance.shotgun)
            shotgun.tag = "unlocked";
        else
            shotgun.tag = "Untagged";

        if (SaveData.Instance.grapplegun)
            grapplegun.tag = "unlocked";
        else
            grapplegun.tag = "Untagged";

        if (SaveData.Instance.richochetgun)
            richochetgun.tag = "unlocked";
          else
            richochetgun.tag = "Untagged";

          if (SaveData.Instance.machinegun)
            machinegun.tag = "unlocked";
          else
            machinegun.tag = "Untagged";

          if (SaveData.Instance.spear)
            spear.tag = "unlocked";
          else
            spear.tag = "Untagged";



    Debug.Log("Loaded PREFS");

    }

    void DebugPref()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();

            //Set curLevel
            SaveData.Instance.currentLevel = SceneManager.GetActiveScene().buildIndex;

            //Delete Weapons
            //PlayerPrefs.SetInt(PlayerPrefsConstants.SHOTGUN, 0);
            //PlayerPrefs.SetInt(PlayerPrefsConstants.GRAPPLEGUN, 0);

            //Save
            PlayerPrefs.Save();

            LoadPref();
        }
    }

    private void unlockAllWeapons()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            shotgun.tag = "unlocked";
            machinegun.tag = "unlocked";
            grapplegun.tag = "unlocked";
            spear.tag = "unlocked"; 
            richochetgun.tag = "unlocked";
        }
    }
}

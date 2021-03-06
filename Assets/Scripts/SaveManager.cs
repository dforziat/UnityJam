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

        //HandgunAmmo
        //PlayerPrefs.SetInt(PlayerPrefsConstants.HANDGUN_AMMO, playerControls.handgunAmmo);

        //ShotgunAmmo;
        // PlayerPrefs.SetInt(PlayerPrefsConstants.SHOTGUN_AMMO, playerControls.shotgunAmmo);


        //Current HP
        //PlayerPrefs.SetInt(PlayerPrefsConstants.CUR_HP, playerControls.curHp);

        //Set curLevel
        PlayerPrefs.SetInt(PlayerPrefsConstants.CUR_LVL, playerControls.curLevel);

        //Set Last Weapon
        PlayerPrefs.SetInt(PlayerPrefsConstants.CUR_WEP, weaponSwitching.curWeapon);


        //Set Weapons
        if (shotgun.tag == "unlocked")
            PlayerPrefs.SetInt(PlayerPrefsConstants.SHOTGUN, 1);

        if (grapplegun.tag == "unlocked")
            PlayerPrefs.SetInt(PlayerPrefsConstants.GRAPPLEGUN, 1);

        if (richochetgun.tag == "unlocked")
            PlayerPrefs.SetInt(PlayerPrefsConstants.RICHOCHETGUN, 1);

        if (machinegun.tag == "unlocked")
            PlayerPrefs.SetInt(PlayerPrefsConstants.MACHINEGUN, 1);

        if (spear.tag == "unlocked")
            PlayerPrefs.SetInt(PlayerPrefsConstants.SPEAR, 1);

        //Save
        PlayerPrefs.Save();
        

    }

    public void LoadPref()
    {
        //Level Start Default Ammo
        //playerControls.handgunAmmo = 20;
       // playerControls.shotgunAmmo = 10;
       // playerControls.curHp = 100;

        if(SceneManager.GetActiveScene().name == "RandomLevel")
        {
            return;
        }

        playerControls.curLevel = PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL);
        weaponSwitching.curWeapon = PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_WEP);

        if (PlayerPrefs.GetInt(PlayerPrefsConstants.SHOTGUN) == 1)
            shotgun.tag = "unlocked";
        else
            shotgun.tag = "Untagged";

        if (PlayerPrefs.GetInt(PlayerPrefsConstants.GRAPPLEGUN) == 1)
            grapplegun.tag = "unlocked";
        else
            grapplegun.tag = "Untagged";

        if (PlayerPrefs.GetInt(PlayerPrefsConstants.RICHOCHETGUN) == 1)
            richochetgun.tag = "unlocked";
          else
            richochetgun.tag = "Untagged";

          if (PlayerPrefs.GetInt(PlayerPrefsConstants.MACHINEGUN) == 1)
            machinegun.tag = "unlocked";
          else
            machinegun.tag = "Untagged";

          if (PlayerPrefs.GetInt(PlayerPrefsConstants.SPEAR) == 1)
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

            //HandgunAmmo
            PlayerPrefs.SetInt(PlayerPrefsConstants.HANDGUN_AMMO, 20);

            //ShotgunAmmo;
            PlayerPrefs.SetInt(PlayerPrefsConstants.SHOTGUN_AMMO, 20);


            //Current HP
            PlayerPrefs.SetInt(PlayerPrefsConstants.CUR_HP, 100);

            //Set curLevel
            PlayerPrefs.SetInt(PlayerPrefsConstants.CUR_LVL, SceneManager.GetActiveScene().buildIndex);

            //Delete Weapons
            PlayerPrefs.SetInt(PlayerPrefsConstants.SHOTGUN, 0);
            PlayerPrefs.SetInt(PlayerPrefsConstants.GRAPPLEGUN, 0);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public int curWeapon;
    public bool lockWeaponSwitch = false;

    private GameObject shotgun;
    private GameObject grapplegun;
    private GameObject machinegun;
    private GameObject ricochetgun;
    private GameObject spear;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "RandomLevel")
        {
            findWeapons();
            unlockWeapons();
        }

        //selectedWeapon = SaveData.Instance.lastWeapon;
        selectedWeapon = curWeapon;
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused || lockWeaponSwitch || PlayerControls.isDead)
        {
            return;
        }
        ChangeWeapon();
    }

    public void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void ChangeWeapon()
    {
        int previousSelectedWeapon = selectedWeapon;
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f || Input.GetButtonDown("WeaponRight"))
        {

            selectedWeapon++;
            if(selectedWeapon >= transform.childCount){
                selectedWeapon = 0;
            }
            while (transform.GetChild(selectedWeapon).tag != "unlocked")
            {
                selectedWeapon++;
                if(selectedWeapon >= transform.childCount)
                {
                 selectedWeapon = 0;
                break;
                 }
            }

        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f || Input.GetButtonDown("WeaponLeft"))
        {
            selectedWeapon--;
            if (selectedWeapon < 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            while (transform.GetChild(selectedWeapon).tag != "unlocked")
            {
                selectedWeapon--;
            }
        }

        //Hand Gun 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        //Shotgun 
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;

        }

        //Grapple Gun 
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }

        //Machine Gun 
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }

        //Ricochet Gun       
        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            selectedWeapon = 4;
        }

        //Spear
        if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6)
         {
            selectedWeapon = 5;
        }

        if (previousSelectedWeapon != selectedWeapon && transform.GetChild(selectedWeapon).tag == "unlocked")
        {
            SelectWeapon();
        }
        SaveData.Instance.lastWeapon = selectedWeapon;

    }

    private void unlockWeapons()
    {
        int currentLevel = SaveData.Instance.currentLevel;


        if (currentLevel > 4)
        {
            shotgun.tag = "unlocked";
        }
        if (currentLevel > 6)
        {
            grapplegun.tag = "unlocked";
        }
        if (currentLevel > 9)
        {
            machinegun.tag = "unlocked";
        }
        if (currentLevel > 12)
        {
            ricochetgun.tag = "unlocked";
        }

        if (currentLevel > 15)
        {
            spear.tag = "unlocked";
        }

        curWeapon = SaveData.Instance.lastWeapon;

        //disable all weapons but handgun for epilogue
        if (SceneManager.GetActiveScene().name == "Epilogue")
        {
            shotgun.tag = "Untagged";
            grapplegun.tag = "Untagged";
            machinegun.tag = "Untagged";
            ricochetgun.tag = "Untagged";
            spear.tag = "Untagged";
            curWeapon = 0;
        }

        //failsafe for weapons that don't belong in early levels
        if ((curWeapon == 5 && currentLevel < 16) ||
            (curWeapon == 4 && currentLevel < 13) ||
            (curWeapon == 3 && currentLevel < 10) ||
            (curWeapon == 2 && currentLevel < 7) ||
            (curWeapon == 1 && currentLevel < 5))
        {
            curWeapon = 0;
        }
    }

    private void findWeapons()
    {
        shotgun = transform.Find("Shotgun").gameObject;
        grapplegun = transform.Find("Grapplegun").gameObject;
        machinegun = transform.Find("Machinegun").gameObject;
        ricochetgun = transform.Find("RicochetGun").gameObject;
        spear = transform.Find("SpearOfBealial").gameObject;
    }
}

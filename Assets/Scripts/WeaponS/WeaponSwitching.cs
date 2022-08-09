using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public int curWeapon;
    public bool lockWeaponSwitch = false;
    public Transform nextWeapon;

    public GameObject shotgun;
    public GameObject grapplegun;
    public GameObject machinegun;
    public GameObject ricochetgun;
    public GameObject spear;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "RandomLevel")
        {
            findWeapons();
            unlockWeapons();
        }

        selectedWeapon = curWeapon;
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused || lockWeaponSwitch)
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
        curWeapon = selectedWeapon;

    }

    private void unlockWeapons()
    {
        int currentLevel = SaveData.Instance.currentLevel;
        
        if(currentLevel > 3)
        {
            shotgun.tag = "unlocked";
            curWeapon = 1;
        }
        if (currentLevel > 5)
        {
            grapplegun.tag = "unlocked";
            curWeapon = 2;
        }
        if (currentLevel > 9)
        {
            machinegun.tag = "unlocked";
            curWeapon = 3;
        }
        if (currentLevel > 12)
        {
            ricochetgun.tag = "unlocked";
            curWeapon = 4;
        }

        if (currentLevel > 15)
        {
            spear.tag = "unlocked";
            curWeapon = 5;
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

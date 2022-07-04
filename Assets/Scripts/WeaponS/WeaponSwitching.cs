using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public int curWeapon;
    public bool lockWeaponSwitch = false;
    public Transform nextWeapon;
    // Start is called before the first frame update
    void Start()
    {
        //this should load in the save manager, but that version doesn't seem to work -Dylan
        curWeapon = PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_WEP);

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
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {

            selectedWeapon++;
            while (transform.GetChild(selectedWeapon).tag != "unlocked")
            {
                selectedWeapon++;
                //new
                if (selectedWeapon >= transform.childCount - 1)
                {
                    selectedWeapon = 0;
                    break;
                }
            }

        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
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

}

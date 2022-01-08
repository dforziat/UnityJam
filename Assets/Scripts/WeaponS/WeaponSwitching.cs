using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0;
    public bool lockWeaponSwitch = false;
    public Transform nextWeapon;
    // Start is called before the first frame update
    void Start()
    {
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
        foreach(Transform weapon in transform)
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
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {              
                while (transform.GetChild(selectedWeapon).tag != "unlocked")
                {
                    selectedWeapon++;
                }
             
            }
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            selectedWeapon--;
            if (selectedWeapon < 0)
            {
                selectedWeapon = transform.childCount - 1;
                if(transform.GetChild(selectedWeapon).tag != "unlocked")
                {
                    selectedWeapon--;
                }
            }
            else
            {
                while (transform.GetChild(selectedWeapon).tag != "unlocked")
                {
                    selectedWeapon--;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            selectedWeapon = 4;
        }
       // if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6)
       // {
       //     selectedWeapon = 5;
       // }

        if (previousSelectedWeapon != selectedWeapon && transform.GetChild(selectedWeapon).tag == "unlocked")
        {
            SelectWeapon();
        }
    }

}

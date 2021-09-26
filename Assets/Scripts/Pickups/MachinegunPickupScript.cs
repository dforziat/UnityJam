using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachinegunPickupScript : MonoBehaviour
{
    public Transform machinegun;
    private GameObject weaponsHud;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            weaponsHud = GameObject.FindGameObjectWithTag("WeaponsHud");
            weaponsHud.GetComponent<WeaponSwitching>().selectedWeapon = 3;
            weaponsHud.GetComponent<WeaponSwitching>().SelectWeapon();
            machinegun.tag = "unlocked";
            Destroy(gameObject);
        }
    }
}

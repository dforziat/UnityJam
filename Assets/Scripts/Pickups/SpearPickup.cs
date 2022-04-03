using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPickup : MonoBehaviour
{
    public Transform spear;
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
            weaponsHud.GetComponent<WeaponSwitching>().selectedWeapon = 5;
            weaponsHud.GetComponent<WeaponSwitching>().SelectWeapon();
            spear.tag = "unlocked";
            Destroy(gameObject);
        }
    }
}

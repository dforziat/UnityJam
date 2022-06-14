using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplegunPickup : MonoBehaviour
{
     Transform grapplegun;
     GameObject weaponsHud;
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
            weaponsHud.GetComponent<WeaponSwitching>().selectedWeapon = 2;
            weaponsHud.GetComponent<WeaponSwitching>().SelectWeapon();
            weaponsHud.transform.Find("Grapplegun").tag = "unlocked";
            Destroy(gameObject);
        }
    }
}

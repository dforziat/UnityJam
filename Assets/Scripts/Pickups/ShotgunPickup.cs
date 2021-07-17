using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPickup : MonoBehaviour
{
    public Transform shotgun;
    public GameObject weaponsHud;
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
            //play special sfx
            weaponsHud.GetComponent<WeaponSwitching>().selectedWeapon = 1;
            weaponsHud.GetComponent<WeaponSwitching>().SelectWeapon();
            shotgun.tag = "unlocked";
            Destroy(gameObject);
        }
    }
}

using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPickup : MonoBehaviour
{
     Transform spear;
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
            weaponsHud.GetComponent<WeaponSwitching>().selectedWeapon = 5;
            weaponsHud.GetComponent<WeaponSwitching>().SelectWeapon();
            weaponsHud.transform.Find("SpearOfBealial").tag = "unlocked";

            if (SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement(SteamAchievementConstants.ARSENAL, out bool achievementUnlocked);
                if (!achievementUnlocked)
                {
                    SteamScript.incrementPlatStat();
                    SteamUserStats.SetAchievement(SteamAchievementConstants.ARSENAL);
                    SteamUserStats.StoreStats();
                }

            }

            Destroy(gameObject);
        }
    }
}

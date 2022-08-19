using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverHealPickup : MonoBehaviour
{
    

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
            PlayerControls playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
            if (playerControls.curHp < playerControls.maxOverHeal)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().OverHeal();
                Destroy(gameObject);
            }

            if (SteamManager.Initialized)
            {
                SteamUserStats.GetAchievement(SteamAchievementConstants.ARMOR, out bool achievementUnlocked);
                if (!achievementUnlocked)
                {
                    SteamScript.incrementPlatStat();
                    SteamUserStats.SetAchievement(SteamAchievementConstants.ARMOR);
                    SteamUserStats.StoreStats();
                }

            }
        }
    }
}

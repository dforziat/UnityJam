using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamScript : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
    }

    void Start()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            SteamUserStats.GetAchievement(SteamAchievementConstants.ACT_1, out bool bossKilled);
            int statNum;
            SteamUserStats.GetStat(SteamAchievementConstants.STAT_PLAT, out statNum);
            Debug.Log("Boss1 Killed: " + bossKilled);
            Debug.Log("Plat Stats: " + statNum);
            Debug.Log(name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //resetAchievements();
    }

    public static void incrementPlatStat()
    {
        int statNum;
        SteamUserStats.GetStat(SteamAchievementConstants.STAT_PLAT, out statNum);
        statNum++;
        SteamUserStats.SetStat(SteamAchievementConstants.STAT_PLAT, statNum);
    }

    private void resetAchievements()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (SteamManager.Initialized)
            {
                SteamUserStats.ResetAllStats(true);
                SteamUserStats.StoreStats();
                SteamUserStats.GetStat(SteamAchievementConstants.STAT_PLAT, out int statNum);
                SteamUserStats.GetAchievement(SteamAchievementConstants.ACT_1, out bool bossKilled);
                Debug.Log("Boss1 Killed: " + bossKilled);
                Debug.Log("Plat Stats: " + statNum);
            }
        }
    }
}

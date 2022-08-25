using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VersionNumberScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SteamManager.Initialized)
        {
            GetComponent<TextMeshProUGUI>().text =  SteamApps.GetAppBuildId().ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

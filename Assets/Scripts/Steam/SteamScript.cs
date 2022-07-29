using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamScript : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (SteamManager.Initialized)
        {
            string name = SteamFriends.GetPersonaName();
            Debug.Log(name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataInitializer : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SaveSystem.load(); 
    }

    private void OnApplicationQuit()
    {
        SaveSystem.save();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    bool hasLoaded = false;
    public static SaveDataInitializer instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        SaveSystem.load();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.save();
    }
}

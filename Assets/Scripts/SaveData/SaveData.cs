using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int currentLevel;
    public int highestLevel;

    public int resolutionWidth;
    public int resolutionHeight;
    public int frameRate;
    public bool fullScreen;

    public float masterVolume;
    public float sfxVolume;
    public float musicVolume;

    public float mouseSens;

    public float[] bestTime;
    public float challengeBestTime;

   public static SaveData Instance;


}

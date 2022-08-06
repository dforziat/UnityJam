using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int currentLevel;
    public int highestLevel;
    public int curWeapon;
    public bool shotgun;
    public bool grapplegun;
    public bool richochetgun;
    public bool machinegun;
    public bool spear;


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

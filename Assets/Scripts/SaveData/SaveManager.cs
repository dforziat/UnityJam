using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    public void SavePrefs()
    {

        //Set Max Level
        int highestLevel = SaveData.Instance.highestLevel;
        int curLevel = SaveData.Instance.currentLevel;
        if (highestLevel < curLevel)
        {
            SaveData.Instance.highestLevel = curLevel;
        }

        //Save
        PlayerPrefs.Save();
        
    }



}

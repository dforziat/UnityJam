using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMainMenu : MonoBehaviour
{
    public GameObject Act1;
    public GameObject Act2;
    public GameObject Act3;

    // if cur level <=6 = act 1 
    // if cur level >6 && <=12 = act 2 
    // if cur level >12 = act 3
    //else act 1

    // Start is called before the first frame update
    void Start()
    {
         int currentLevel = 1;
        if(SaveData.Instance != null)
        {
            currentLevel = SaveData.Instance.currentLevel;
        }
        if (currentLevel <= 7)
        {
            Act1.SetActive(true);
            Act2.SetActive(false);
            Act3.SetActive(false);
            return;
        }
        else if (currentLevel > 7 && currentLevel <= 13)
        {
            Act1.SetActive(false);
            Act2.SetActive(true);
            Act3.SetActive(false);
            return;
        }
        else if (currentLevel > 13)
        {
            Act1.SetActive(false);
            Act2.SetActive(false);
            Act3.SetActive(true);
            return;
        }
        else
        {
            Act1.SetActive(true);
            Act2.SetActive(false);
            Act3.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

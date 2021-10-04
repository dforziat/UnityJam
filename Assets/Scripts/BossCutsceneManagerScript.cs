using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCutsceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas playerCanvas;
    public GameObject boss;
    public DoorProximityScript entranceDoor;
    public AudioSource musicManager;
    public Text bossName;
    public Text bossSubName;


    void Start()
    {
        boss.SetActive(false);
        playerCanvas.enabled = false;
        bossName.enabled = false;
        bossSubName.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartFight()
    {
        playerCanvas.enabled = true;
        entranceDoor.isUnlocked = false;
    }

    public void ActivateBoss()
    {
        boss.SetActive(true);
        bossName.enabled = true;
        bossName.CrossFadeAlpha(0, 8, false);
        bossSubName.enabled = true;
        bossSubName.CrossFadeAlpha(0, 8, false);
        musicManager.Play();
    }
}

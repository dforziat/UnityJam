using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCutsceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GUI;
    public GameObject boss;
    public DoorProximityScript entranceDoor;
    public AudioSource musicManager;
    public Text bossName;
    public Text bossSubName;


    void Start()
    {
        boss.SetActive(false);
        GUI.SetActive(false);
        bossName.enabled = false;
        bossSubName.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartFight()
    {
        GUI.SetActive(true);
        entranceDoor.isUnlocked = false;
        boss.GetComponent<Animator>().SetTrigger("enterRoom");
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

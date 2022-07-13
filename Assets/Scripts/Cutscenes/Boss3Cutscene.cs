using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boss3Cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public GameObject GUI;
    public GameObject boss;
    public DoorProximityScript entranceDoor;
    public AudioSource musicManager;
    public TextMeshProUGUI bossName;
    public TextMeshProUGUI bossSubName;
    public Image healthBar;
    public GlassWallScript[] glassPods;
    void Start()
    {
        GUI.SetActive(false);
        bossName.enabled = false;
        bossSubName.enabled = false;
        healthBar.enabled = false;
        musicManager.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateBoss()
    {
        bossName.enabled = true;
        bossName.CrossFadeAlpha(0, 8, false);
        bossSubName.enabled = true;
        bossSubName.CrossFadeAlpha(0, 8, false);
        musicManager.Play();

        healthBar.enabled = true;
        healthBar.GetComponent<Animator>().SetTrigger("healthBarFill");

        foreach(GlassWallScript glassPod in glassPods)
        {
            glassPod.activateAnimation();
        }

    }

    public void StartFight()
    {
        GUI.SetActive(true);
        entranceDoor.isUnlocked = false;
        boss.GetComponent<Boss3Script>().startFight();
    }

}

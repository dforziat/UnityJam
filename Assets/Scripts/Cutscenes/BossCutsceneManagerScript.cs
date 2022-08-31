using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossCutsceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject GUI;
    public GameObject boss;
    public DoorProximityScript entranceDoor;
    public AudioSource musicManager;
    public TextMeshProUGUI bossName;
    public TextMeshProUGUI bossSubName;
    public Image healthBar;
    public GameObject bossShadow;


    void Start()
    {
        boss.SetActive(false);
        GUI.SetActive(false);
        bossName.enabled = false;
        bossSubName.enabled = false;
        healthBar.enabled = false;
        bossShadow.SetActive(false);
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
        GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<WeaponSwitching>().curWeapon = 0;
        GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<WeaponSwitching>().selectedWeapon = 0;
        GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<WeaponSwitching>().SelectWeapon();

    }

    public void ActivateBoss()
    {
        boss.SetActive(true);
        boss.GetComponent<LineRenderer>().enabled = false;
        bossName.enabled = true;
        bossName.CrossFadeAlpha(0, 8, false);
        bossSubName.enabled = true;
        bossSubName.CrossFadeAlpha(0, 8, false);
        musicManager.Play();

        healthBar.enabled = true;
        healthBar.GetComponent<Animator>().SetTrigger("healthBarFill");
        bossShadow.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RicochetScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canShoot = true;
    private bool onFirstLoad = true;
    float rateOfFire = 1f;
    PlayerControls playerControls;
    Animator gunAnimator;
    AudioSource audioSource;
    private WeaponSwitching weaponSwitching;
    public Camera cam;


    public Image batteryIcon;
    public Image infinityAmmoIcon;
    public AudioClip weaponSwitchClip;
    public AudioClip dryfireClip;
    public AudioClip shootClip;
    public AudioClip rechargeCompleteClip;
    public AudioClip rechargeTickClip;
    public GameObject ricochetBall;
    public GameObject player;

    void Start()
    {
        weaponSwitching = GetComponentInParent<WeaponSwitching>();
        gunAnimator = GetComponent<Animator>();

    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        checkWeaponSwitchAudio();
        batteryIcon.enabled = true;
        infinityAmmoIcon.enabled = true;
        canShoot = true;

    }

    private void OnDisable()
    {
        if (weaponSwitching != null)
        {
            weaponSwitching.lockWeaponSwitch = false;
        }
        infinityAmmoIcon.enabled = false;
        batteryIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused || LevelManager.levelLoading || PlayerControls.isDead)
        {
            return;
        }
        shootGun();
    }


    public void shootGun()
    {

        if (Input.GetMouseButtonDown(0) && canShoot && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("RichochetGun_Idle"))
        {
            StartCoroutine(Shoot());
        }
        if (Input.GetMouseButtonDown(0) && !canShoot)
        {
            audioSource.PlayOneShot(dryfireClip);
        }

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        gunAnimator.SetTrigger("shoot");
        audioSource.PlayOneShot(shootClip);

        Instantiate(ricochetBall, player.transform.position, player.transform.rotation);
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }

    public void rechargeComplete()//call from animation event
    {
        gunAnimator.ResetTrigger("shoot");
        audioSource.PlayOneShot(rechargeCompleteClip);
        gunAnimator.SetBool("shooting", false);
        canShoot = true;
    }

    private void checkWeaponSwitchAudio()
    {
        if (!onFirstLoad)
        {
            audioSource.PlayOneShot(weaponSwitchClip);
        }
        else
        {
            onFirstLoad = false;
        }
    }

    private void playRechargeTickSFX()
    {
        audioSource.PlayOneShot(rechargeTickClip);
    }

    private void playRechargeCompleteSFX()
    {
        audioSource.PlayOneShot(rechargeCompleteClip);
    }
}


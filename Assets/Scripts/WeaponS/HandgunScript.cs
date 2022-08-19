using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandgunScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canShoot = true;
    private bool onFirstLoad = true;
    int damage = 1;
    float rateOfFire = .15f;
    Animator gunAnimator;
    PlayerControls playerControls;
    CameraEffects cameraEffects;
    AudioSource audioSource;

    public AudioClip shootClip;
    public AudioClip weaponSwitchClip;
    public AudioClip dryfireClip;
    public TextMeshProUGUI handgunAmmoText;
    public Image ammoIcon;
    public Camera cam;
    public GameObject hitEffect;

    void Start()
    {
        gunAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        cameraEffects = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraEffects>();
        checkWeaponSwitchAudio();
        handgunAmmoText.enabled = true;
        handgunAmmoText.text = playerControls.handgunAmmo.ToString("D3");
        ammoIcon.enabled = true;
        canShoot = true;
    }

    private void OnDisable()
    {
        handgunAmmoText.enabled = false;
        ammoIcon.enabled = false;
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

        if (Input.GetButtonDown("Fire1")  && canShoot && playerControls.handgunAmmo > 0 && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gun__Idle"))
        {
            StartCoroutine(Shoot());
        }
        if(Input.GetMouseButtonDown(0) && playerControls.handgunAmmo <= 0)
        {
            audioSource.PlayOneShot(dryfireClip);
        }

    }

    IEnumerator Shoot() 
    {
        canShoot = false;
        gunAnimator.SetTrigger("shoot");
        audioSource.PlayOneShot(shootClip);
        playerControls.handgunAmmo--;
        handgunAmmoText.text = playerControls.handgunAmmo.ToString("D3");
        cameraEffects.gunShake();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, .3f);

            if (hit.transform.tag == "Enemy")
            {
                hit.transform.SendMessageUpwards("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
            if (hit.transform.tag == "Button")
            {
                hit.transform.GetComponent<ButtonController>().Activate();
            }
            if (hit.transform.tag == "FinalButton")
            {
                hit.transform.GetComponent<EpilogueButton>().Activate();
            }
        }
        yield return new WaitForSeconds(rateOfFire);
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

}

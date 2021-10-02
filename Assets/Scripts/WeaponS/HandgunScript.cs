using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandgunScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canShoot = true;
    int damage = 1;
    float rateOfFire = .2f;
    Animator gunAnimator;
    PlayerControls playerControls;
    AudioSource audioSource;

    public AudioClip shootClip;
    public AudioClip weaponSwitchClip;
    public AudioClip dryfireClip;
    public Text handgunAmmoText;
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
        audioSource.PlayOneShot(weaponSwitchClip);
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
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

        if (Input.GetMouseButtonDown(0) && canShoot && playerControls.handgunAmmo > 0 && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gun__Idle"))
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
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, .3f);

            if (hit.transform.tag == "Enemy")
            {
                hit.transform.BroadcastMessage("takeDamage", damage);
            }
            if (hit.transform.tag == "Button")
            {
                hit.transform.GetComponent<ButtonController>().Activate();
            }
        }
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }


}

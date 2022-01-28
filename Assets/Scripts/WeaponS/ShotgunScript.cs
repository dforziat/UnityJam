using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShotgunScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canShoot = true;
    private bool onFirstLoad = true;
    int damage = 1;
    float rateOfFire = .5f;
    Animator gunAnimator;
    int buckshot = 4;
    float spread = .1f;
    PlayerControls playerControls;
    AudioSource audioSource;

    public AudioClip shootClip;
    public AudioClip weaponSwitchClip;
    public AudioClip dryfireClip;
    public Image ammoIcon;
    public TextMeshProUGUI shotgunAmmoText;
    public Camera cam;
    public GameObject hitEffect;


    void Start()
    {
        gunAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        checkWeaponSwitchAudio();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        shotgunAmmoText.enabled = true;
        shotgunAmmoText.text = playerControls.shotgunAmmo.ToString("D3");
        ammoIcon.enabled = true;
        canShoot = true;
    }

    private void OnDisable()
    {
        shotgunAmmoText.enabled = false;
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
        if (Input.GetMouseButtonDown(0) && canShoot && playerControls.shotgunAmmo > 0 && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunIdle"))
        {
            StartCoroutine(Shoot());
        }
        if (Input.GetMouseButtonDown(0) && playerControls.shotgunAmmo <= 0)
        {
            audioSource.PlayOneShot(dryfireClip);
        }

    }

    IEnumerator Shoot() 
    {
        canShoot = false;
        gunAnimator.SetTrigger("shoot");
        audioSource.PlayOneShot(shootClip);
        playerControls.shotgunAmmo--;
        shotgunAmmoText.text = playerControls.shotgunAmmo.ToString("D3");
        RaycastHit hit;

        for(int i = 0; i < buckshot; i++)
        {
            Vector3 randomSpread = cam.transform.forward + cam.transform.right * Random.Range(-spread, spread);
            if (Physics.Raycast(cam.transform.position, randomSpread, out hit))
            {
                GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, .3f);

                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.BroadcastMessage("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
                }
                if (hit.transform.tag == "Button")
                {
                    hit.transform.GetComponent<ButtonController>().Activate();
                }
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

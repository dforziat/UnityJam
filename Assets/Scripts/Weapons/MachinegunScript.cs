using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MachinegunScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canShoot = true;
    private bool onFirstLoad = true;
    int damage = 1;
    float rateOfFire = .1f;
    float spread = .05f;
    Animator gunAnimator;
    PlayerControls playerControls;
    AudioSource audioSource;

    public AudioClip shootClip;
    public AudioClip weaponSwitchClip;
    public AudioClip dryfireClip;
    public TextMeshProUGUI machinegunAmmoText;
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
        checkWeaponSwitchAudio();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        machinegunAmmoText.enabled = true;
        machinegunAmmoText.text = playerControls.machinegunAmmo.ToString("D3");
        ammoIcon.enabled = true;
        canShoot = true;
    }

    private void OnDisable()
    {
        machinegunAmmoText.enabled = false;
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
        if (Input.GetMouseButton(0) && canShoot && playerControls.machinegunAmmo > 0 && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gun_Idle"))
        {
            StartCoroutine(Shoot());
        }
        if (Input.GetMouseButtonDown(0) && playerControls.machinegunAmmo <= 0)
        {
            audioSource.PlayOneShot(dryfireClip);
        }

    }

    IEnumerator Shoot() 
    {
        canShoot = false;
        PickRandomMuzzleFlash();
        if (audioSource.isPlaying)
        {
           // audioSource.Stop();
        }
        audioSource.PlayOneShot(shootClip);
        playerControls.machinegunAmmo--;
        machinegunAmmoText.text = playerControls.machinegunAmmo.ToString("D3");
        Vector3 randomSpread = cam.transform.forward + cam.transform.right * Random.Range(-spread, spread);
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, randomSpread, out hit))
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, .3f);

            if (hit.transform.tag == "Enemy")
            {
                if(hit.transform != null)
                {
                    hit.transform.SendMessageUpwards("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
                }
            }
            if (hit.transform.tag == "Button")
            {
                hit.transform.GetComponent<ButtonController>().Activate();
            }
        }
        yield return new WaitForSeconds(rateOfFire);
        gunAnimator.SetInteger("shoot", 0);
        canShoot = true;
    }

    public void PickRandomMuzzleFlash()
    {
        int muzzleFlashNum = Random.Range(1, 4);//Random range of 1,2,3
        gunAnimator.SetInteger("shoot", muzzleFlashNum);
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

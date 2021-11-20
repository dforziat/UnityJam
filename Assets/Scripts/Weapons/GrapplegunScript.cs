using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplegunScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canShoot = true;
    private bool onFirstLoad = true;
    int damage = 3;
    float rateOfFire = .5f;
    float laserYPosOffset = .1f;
    Animator gunAnimator;
    PlayerControls playerControls;
    AudioSource audioSource;
    private LineRenderer lineRenderer;
    private WeaponSwitching weaponSwitching;

    public AudioClip shootClip;
    public AudioClip weaponSwitchClip;
    public AudioClip dryfireClip;
    public AudioClip rechargeTickClip;
    public AudioClip rechargeCompleteClip;
    public Image ammoIcon;
    public Text grapplegunAmmoText;
    public Camera cam;
    public GameObject hitEffect;


    void Start()
    {
        weaponSwitching = GetComponentInParent<WeaponSwitching>();
        gunAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
        checkWeaponSwitchAudio();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        grapplegunAmmoText.enabled = true;
        ammoIcon.enabled = true;
        lineRenderer.enabled = false;
        canShoot = true;
    }

    private void OnDisable()
    {
        if(weaponSwitching != null)
        {
            weaponSwitching.lockWeaponSwitch = false;
        }  
        grapplegunAmmoText.enabled = false;
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
        if (Input.GetMouseButtonDown(0) && canShoot && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("GrapplegunIdle"))
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
        weaponSwitching.lockWeaponSwitch = true;
        canShoot = false;
        gunAnimator.SetBool("shooting", true);
        audioSource.PlayOneShot(shootClip);

        RaycastHit preHit;
        LayerMask wallMask = LayerMask.GetMask("Wall");
        float range = 0;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out preHit, Mathf.Infinity, wallMask))
        {
            range = preHit.distance;
        }

        RaycastHit finalHit = preHit;

        RaycastHit[] hits;

        hits = Physics.RaycastAll(cam.transform.position, cam.transform.forward, range);

        foreach(RaycastHit hit in hits){
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.BroadcastMessage("takeDamage", damage, SendMessageOptions.DontRequireReceiver);
            }
            if (hit.transform.tag == "Button")
            {
                hit.transform.GetComponent<ButtonController>().Activate();
            }
            if (hit.transform.tag == "GrapplePoint")
            {
                playerControls.isGrappling = true;
                finalHit = hit;
            }
            if(hit.transform.tag == "GrapplePoint" || hit.transform.tag == "Untagged")
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, new Vector3(cam.transform.position.x, cam.transform.position.y - laserYPosOffset, cam.transform.position.z) + cam.transform.forward / 2);

                lineRenderer.SetPosition(1, hit.point);

                GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, .3f);
            }
        }

        Debug.Log("Grapple hit tag: " + finalHit.transform.tag);
        yield return new WaitForSeconds(rateOfFire);

        if (finalHit.transform == null || !finalHit.transform.CompareTag("GrapplePoint"))
        {
            ShootingRecovery();
        }
     
    }

    public void ShootingRecovery()//CALL FROM GRAPPLE POINT COLLISION TO RESET SHOOTING
    {
        gunAnimator.SetTrigger("recharge");
        lineRenderer.enabled = false;
    }

    public void rechargeComplete()//call from animation event
    {
        gunAnimator.ResetTrigger("recharge");
        weaponSwitching.lockWeaponSwitch = false;
        audioSource.PlayOneShot(rechargeCompleteClip);
        gunAnimator.SetBool("shooting", false);
        canShoot = true;
    }

    public void playClickSound()
    {
        audioSource.PlayOneShot(rechargeTickClip);
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

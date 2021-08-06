using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplegunScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canShoot = true;
    int damage = 2;
    float rateOfFire = .5f;
    float laserYPosOffset = .1f;
    Animator gunAnimator;
    PlayerControls playerControls;
    AudioSource audioSource;
    private LineRenderer lineRenderer;
    private WeaponSwitching weaponSwitching;

    public AudioClip shootClip;
    public AudioClip weaponSwitchClip;
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
        audioSource.PlayOneShot(weaponSwitchClip);
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        grapplegunAmmoText.enabled = true;
        grapplegunAmmoText.text = playerControls.grapplegunAmmo.ToString("D3");
        ammoIcon.enabled = true;
        lineRenderer.enabled = false;
        canShoot = true;
    }

    private void OnDisable()
    {
        weaponSwitching.lockWeaponSwitch = false;
        grapplegunAmmoText.enabled = false;
        ammoIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            return;
        }
        shootGun();
    }


    public void shootGun()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && playerControls.grapplegunAmmo > 0 && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("GrapplegunIdle"))
        {
            Debug.Log("Im Shooting");
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot() 
    {
        weaponSwitching.lockWeaponSwitch = true;
        canShoot = false;
        gunAnimator.SetBool("shooting", true);
        audioSource.PlayOneShot(shootClip);
        playerControls.grapplegunAmmo--;
        grapplegunAmmoText.text = playerControls.grapplegunAmmo.ToString("D3");
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, new Vector3(cam.transform.position.x, cam.transform.position.y - laserYPosOffset, cam.transform.position.z) + cam.transform.forward/2);
            lineRenderer.SetPosition(1, hit.point);

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
            if(hit.transform.tag == "GrapplePoint")
            {
                Debug.Log("Started Grapple");
                playerControls.isGrappling = true;
            }
        }

        yield return new WaitForSeconds(rateOfFire);
        if (hit.transform == null || !hit.transform.CompareTag("GrapplePoint"))
        {
            weaponSwitching.lockWeaponSwitch = false;
            gunAnimator.SetBool("shooting", false);
            lineRenderer.enabled = false;
            canShoot = true;
        }
    }

    public void ShootingRecovery()//CALL FROM GRAPPLE POINT COLLISION TO RESET SHOOTING
    {
        weaponSwitching.lockWeaponSwitch = false;
        gunAnimator.SetBool("shooting", false);
        lineRenderer.enabled = false;
        canShoot = true;
    }
}

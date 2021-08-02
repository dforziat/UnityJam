using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplegunScript : MonoBehaviour
{
    // Start is called before the first frame update

    bool canShoot = true;
    int damage = 1;
    float grappleSpeed = 2f;
    float rateOfFire = .5f;
    Animator gunAnimator;
    PlayerControls playerControls;
    AudioSource audioSource;

    public AudioClip shootClip;
    public AudioClip weaponSwitchClip;
    public Image ammoIcon;
    public Text grapplegunAmmoText;
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
        grapplegunAmmoText.enabled = true;
        grapplegunAmmoText.text = playerControls.grapplegunAmmo.ToString("D3");
        ammoIcon.enabled = true;
        canShoot = true;
    }

    private void OnDisable()
    {
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
        canShoot = false;
        gunAnimator.SetBool("shooting", true);
        audioSource.PlayOneShot(shootClip);
        playerControls.grapplegunAmmo--;
        grapplegunAmmoText.text = playerControls.grapplegunAmmo.ToString("D3");
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
            if(hit.transform.tag == "GrapplePoint")
            {
                playerControls.StartGrapple(grappleSpeed);
            }
        }

        yield return new WaitForSeconds(rateOfFire);
        gunAnimator.SetBool("shooting", false);
        canShoot = true;
    }
}

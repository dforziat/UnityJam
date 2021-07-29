using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunScript : MonoBehaviour
{
    // Start is called before the first frame update

    bool canShoot = true;
    int damage = 1;
    float rateOfFire = .5f;
    Animator gunAnimator;
    int buckshot = 4;
    float spread = .1f;
    PlayerControls playerControls;
    AudioSource audioSource;

    public AudioClip shootClip;
    public AudioClip weaponSwitchClip;
    public Image ammoIcon;
    public Text ammoText;
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
        ammoText.text = playerControls.shotgunAmmo.ToString("D3");
        ammoIcon.enabled = true;
        canShoot = true;
    }

    private void OnDisable()
    {
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
        if (Input.GetMouseButtonDown(0) && canShoot && playerControls.shotgunAmmo > 0 && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("ShotgunIdle"))
        {
            Debug.Log("Im Shooting");
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot() 
    {
        canShoot = false;
        gunAnimator.SetTrigger("shoot");
        audioSource.PlayOneShot(shootClip);
        playerControls.shotgunAmmo--;
        ammoText.text = playerControls.shotgunAmmo.ToString("D3");
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
                    hit.transform.BroadcastMessage("takeDamage", damage);
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
}

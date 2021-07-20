using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandgunScript : MonoBehaviour
{
    // Start is called before the first frame update

    bool canShoot = true;
    int damage = 1;
    float rateOfFire = .5f;
    Animator gunAnimator;
    PlayerControls playerControls;

    public Text ammoText;
    public Image ammoIcon;
    public Camera cam;
    public GameObject hitEffect;

    void Start()
    {
        gunAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        ammoText.text = playerControls.handgunAmmo.ToString("D3");
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
        shootGun();
    }


    public void shootGun()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && playerControls.handgunAmmo > 0 && gunAnimator.GetCurrentAnimatorStateInfo(0).IsName("Gun__Idle"))
        {
            Debug.Log("Im Shooting");
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot() 
    {
        canShoot = false;
        gunAnimator.SetTrigger("shoot");
        GetComponent<AudioSource>().Play();
        playerControls.handgunAmmo--;
        ammoText.text = playerControls.handgunAmmo.ToString("D3");
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

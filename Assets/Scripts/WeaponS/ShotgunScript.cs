using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotgunScript : MonoBehaviour
{
    // Start is called before the first frame update

    bool canShoot = true;
    PlayerControls playerControls;
    int damage = 1;
    float rateOfFire = .5f;
    Animator gunAnimator;
    int buckshot = 4;
    float spread = .1f;

    public Text ammoText;
    public Camera cam;
    public GameObject hitEffect;

    void Start()
    {
        gunAnimator = GetComponent<Animator>();
        playerControls = FindObjectOfType<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        shootGun();
    }


    public void shootGun()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && playerControls.handgunAmmo > 0)
        {
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

        for(int i = 0; i < buckshot; i++)
        {
            Vector3 randomSpread = cam.transform.forward + cam.transform.right * Random.Range(-spread, spread);
            if (Physics.Raycast(cam.transform.position, randomSpread, out hit))
            {
                GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, .3f);

                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<EnemyController>().takeDamage(damage);
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

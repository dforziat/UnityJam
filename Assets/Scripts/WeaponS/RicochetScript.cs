using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RicochetScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canShoot = true;
    private bool onFirstLoad = true;
    float rateOfFire = 1f;
    PlayerControls playerControls;

    public Camera cam;


    public GameObject ricochetBall;
    public GameObject player;

    void Start()
    {

    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

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

        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }


    }

    IEnumerator Shoot()
    {
        canShoot = false;
        Instantiate(ricochetBall, player.transform.position, player.transform.rotation);
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3AnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject rifle;
    //Rifle stats
    Vector3 rifleRunPosition = new Vector3(1.2f, 0.56f, 0.67f);
    Vector3 rifleRunRotation = new Vector3(285, 90, 0);

    Vector3 rifleShootPosition = new Vector3(0.589999974f, 0.637000024f, 1.528f);
    Vector3 rifleShootRotation = new Vector3(293.405579f, 58.6852112f, 343.351044f);

    public ParticleSystem muzzleFlash;

    public Transform muzzle;

    public GameObject rifleBullet;
    private float rifleBulletSpeed = 10f;


    public GameObject saber;
    public GameObject saberSlashIndicator;
    public GameObject sabeJumpIndictor;

    public GameObject grenade;
    public Transform grenadeLaunchLocation;
    private float grenadeLaunchPower = 10f;

    private AudioSource audioSource;
    [Header("Audio")]
    public AudioClip rifleShootClip;
    public AudioClip runClip;
    public AudioClip[] saberAttacks;

    private Transform target;


    void Start()
    {
        saber.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void moveRifleShoot()
    {
        rifle.transform.localPosition = rifleShootPosition;
        rifle.transform.localEulerAngles = rifleShootRotation;
    }

    public void disableRifle()
    {
        rifle.SetActive(false);
    }

    public void enableRifle()
    {
        rifle.SetActive(true);
    }

    public void enableSaber()
    {
        saber.SetActive(true);
    }

    public void disableSaber()
    {
        saber.SetActive(false);
    }

    public void rifleShoot()
    {
        enableRifle();
        muzzleFlash.Play();
        audioSource.PlayOneShot(rifleShootClip);
        shootRifleBullet();
    }

    private void shootRifleBullet()
    {
        GameObject rifleBullet = Instantiate(this.rifleBullet);
        rifleBullet.transform.position = muzzle.position;
        var dir = (target.position - rifleBullet.transform.position).normalized;
        rifleBullet.GetComponent<Rigidbody>().velocity = dir * rifleBulletSpeed;
    }

    private void throwGrenade()
    {
        disableRifle();
        disableSaber();

        GameObject grenade = Instantiate(this.grenade);
        grenade.GetComponent<Bomb>().isGrenade = true;
        grenade.transform.position = grenadeLaunchLocation.position;
        var dir = (target.position - grenade.transform.position).normalized;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        grenade.GetComponent<Rigidbody>().velocity = dir * distanceToTarget;

    }

    public void playRandomSaberSound()
    {
        int randomNum = Random.Range(0, saberAttacks.Length);
        audioSource.PlayOneShot(saberAttacks[randomNum]);
    }


    public void drawSword()
    {
        disableRifle();
        enableSaber();
    }

    public void drawRifle()
    {
        enableRifle();
        disableSaber();
    }


}

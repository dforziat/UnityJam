using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour

{
    // Start is called before the first frame update
    private bool canShoot = true;
    private bool onFirstLoad = true;
    float rateOfFire = .5f;
    //Animator gunAnimator;

    PlayerControls playerControls;
    WeaponSwitching weaponSwitching;

    //AudioSource audioSource;
    //public AudioClip shootClip;
    //public AudioClip weaponSwitchClip;
    //public AudioClip dryfireClip;

    //public Image ammoIcon;
    // public Text spearAmmoText;
    public Camera cam;
    //public GameObject hitEffect;


    [SerializeField] CharacterController controller;
    public Vector3 dir;
   
    int dashSpeed = 15;
    float dashTime = .3f;

    public GameObject dashHurtbox;
    public GameObject Player;
    public GameObject dashEffect;

    // Start is called before the first frame update
    void Start()
    {
        //gunAnimator = GetComponent<Animator>();
        weaponSwitching = GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<WeaponSwitching>();


    }

    private void OnEnable()
    {
        //audioSource = GetComponent<AudioSource>();
       // checkWeaponSwitchAudio();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        //spearAmmoText.enabled = true;
        //spearAmmoText.text = playerControls.shotgunAmmo.ToString("D3");
        //ammoIcon.enabled = true;
        canShoot = true;
    }

    private void OnDisable()
    {
        //spearAmmoText.enabled = false;
        //ammoIcon.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused || LevelManager.levelLoading || PlayerControls.isDead)
        {
            return;
        }
        dash();
    }

    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time;
        //spearTimer.canShoot = false; <- this script needs to be made still
        dashEffect.SetActive(true);
        //gunAnimator.SetTrigger("shoot");
        //audioSource.PlayOneShot(shootClip);
        //playerControls.spearAmmo--;
        //spearAmmoText.text = playerControls.shotgunAmmo.ToString("D3");

        var clonedashHurtbox = Instantiate(dashHurtbox, Player.transform.position, Player.transform.rotation, Player.transform);
        while (Time.time < startTime + dashTime)
        {
            dir = (Player.transform.forward * 1);
            controller.Move(dir * dashSpeed * Time.deltaTime);
            weaponSwitching.lockWeaponSwitch = true;
            yield return null;
        }
        Destroy(clonedashHurtbox);
        dashEffect.SetActive(false);
        canShoot = true; // <- remove this when timer script is added
        weaponSwitching.lockWeaponSwitch = false;

    }

    void dash()
    {
        if (Input.GetMouseButtonDown(0)) //&& spearTimer.canShoot==true; <- this script needs to be made still
        {
            StartCoroutine(DashCoroutine());
            //spearTimer.startTimer(); <- this script needs to be made still

        }
    }

    private void checkWeaponSwitchAudio()
    {
       // if (!onFirstLoad)
      //  {
      //      audioSource.PlayOneShot(weaponSwitchClip);
     //   }
     //   else
   //     {
    //        onFirstLoad = false;
     //   }
    }
}

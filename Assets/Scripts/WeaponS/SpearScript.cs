using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpearScript : MonoBehaviour

{
    // Start is called before the first frame update
    private bool canShoot = true;
    private bool onFirstLoad = true;
    float rateOfFire = .5f;
    //Animator gunAnimator;

    PlayerControls playerControls;
    WeaponSwitching weaponSwitching;
    SpearTimer spearTimer;

    //AudioSource audioSource;
    //public AudioClip shootClip;
    //public AudioClip weaponSwitchClip;
    //public AudioClip dryfireClip;

    public Image ammoIcon;
    public Image infinityAmmoIcon;
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
        spearTimer = GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<SpearTimer>();


    }

    private void OnEnable()
    {
        //audioSource = GetComponent<AudioSource>();
       // checkWeaponSwitchAudio();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        infinityAmmoIcon.enabled = true;
        ammoIcon.enabled = true;
        
    }

    private void OnDisable()
    {
        infinityAmmoIcon.enabled = false;
        ammoIcon.enabled = false;
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
        spearTimer.canShoot = false; 
        dashEffect.SetActive(true);
        //gunAnimator.SetTrigger("shoot");
        //audioSource.PlayOneShot(shootClip);

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
        weaponSwitching.lockWeaponSwitch = false;

    }

    void dash()
    {
        if (Input.GetMouseButtonDown(0) && spearTimer.canShoot == true)
        {
            StartCoroutine(DashCoroutine());
            spearTimer.startTimer();

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

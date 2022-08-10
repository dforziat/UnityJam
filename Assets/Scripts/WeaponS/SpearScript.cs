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
    Animator animator;

    PlayerControls playerControls;
    WeaponSwitching weaponSwitching;
    SpearTimer spearTimer;

    AudioSource audioSource;
    AudioSource attackAudioSource;
    public AudioClip attackClip;
    public AudioClip electricClip;
    //public AudioClip dryfireClip;

    public Image ammoIcon;
    public Image infinityAmmoIcon;
    private Camera cam;
    //public GameObject hitEffect;


    [SerializeField] CharacterController controller;
    public Vector3 dir;
   
    int dashSpeed = 15;
    float dashTime = .3f;

    public GameObject dashHurtbox;
    private GameObject Player;
    private GameObject dashEffect;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        weaponSwitching = GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<WeaponSwitching>();
        spearTimer = GameObject.FindGameObjectWithTag("WeaponsHud").GetComponent<SpearTimer>();
        cam = Player.transform.Find("Main Camera").GetComponent<Camera>();
        dashEffect = Player.transform.Find("DashEffect").gameObject;
    }

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        attackAudioSource = GetComponent<AudioSource>();
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
        animator.SetTrigger("attack");
        audioSource.PlayOneShot(attackClip);
        attackAudioSource.PlayOneShot(electricClip);

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
        if (Input.GetButtonDown("Fire1") && spearTimer.canShoot == true)
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

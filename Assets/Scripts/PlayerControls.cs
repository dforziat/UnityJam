using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    float moveSpeed = 5;

    [Header("Gravity")]
    float grav = -18f;
    [SerializeField] Transform groundCheck;
    float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;

    [Header("Camera")]
    [SerializeField] float lookSensitivity;

    public int curHp = 50;
    public int maxHp = 100;
    public int currentAmmo = 20;
    public int gunDamage = 1;
    private bool isDead = false;

    public Text hpText;
    public Text ammoText;


    Camera cam;

    Vector3 vel;
    [SerializeField] CharacterController controller;

    public Vector3 dir;

    public GameObject hitEffect;
    public GameObject gun;
    public Animator gunAnimator;
    public Image damageEffect;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip injuredAudioClip;
    public AudioClip deathAudioClip;
    public AudioClip ammoPickupClip;
    public AudioClip healthPickupClip;

    bool canShoot = true;
    float rateOfFire = .5f;
    void Awake()
    {
        // get the components
        cam = Camera.main;
        //disable cursor
        Cursor.lockState = CursorLockMode.Locked;

        isDead = false;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        curHp = maxHp;
        hpText.text = curHp.ToString("D3");
        ammoText.text = currentAmmo.ToString("D3");
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
            Gravity();
            CamLook();
            Movement();
            shootGun();
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 dir = (transform.right * x + transform.forward * z).normalized;
        controller.Move(dir * moveSpeed * Time.deltaTime);
    }

    void CamLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        transform.eulerAngles += Vector3.up * y;
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           // vel.y = Mathf.Sqrt(jumpHeight * -2f * grav);
        }

        vel.y += grav * Time.deltaTime;
        controller.Move(vel * Time.deltaTime);
        if (isGrounded && vel.y < 0)
        {
            vel.y = -2f;
        }
    }

    public void TakeDamage(int damage)
    {
        GetComponent<DamageEffect>().ShowDamageEffect();
        audioSource.PlayOneShot(injuredAudioClip);
        curHp -= damage;
        hpText.text = curHp.ToString("D3");
        if (curHp <= 0)
        {
            damageEffect.enabled = false;
            hpText.text = "000";
            GetComponent<DeathHandler>().HandleDeath();
            audioSource.PlayOneShot(deathAudioClip);
            isDead = true;
        }
    }

    public void Heal(int healAmount)
    {
        if (curHp + healAmount > maxHp)
        {
            curHp = maxHp;
        }
        else
        {
            curHp += healAmount;
        }
        hpText.text = curHp.ToString("D3");
        audioSource.PlayOneShot(healthPickupClip);
    }

    public void RecieveAmmo(int ammoAmount)
    {
        audioSource.PlayOneShot(ammoPickupClip);
        currentAmmo += ammoAmount;
        ammoText.text = currentAmmo.ToString("D3");
    }


    public void shootGun()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && currentAmmo > 0)
        {
            StartCoroutine(Shoot());
        }

    }

    IEnumerator Shoot()
    {
        canShoot = false;
        gunAnimator.SetTrigger("shoot");
        gun.GetComponent<AudioSource>().Play();
        currentAmmo--;
        ammoText.text = currentAmmo.ToString("D3");
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, .3f);

            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyController>().takeDamage(gunDamage);
            }
        }
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;
    }




}
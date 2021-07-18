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

    public int curHp = 90;
    public int maxHp = 100;
    private bool isDead = false;

    public Text hpText;
    public Text ammoText;

    public int handgunAmmo = 20;
    public int shotgunAmmo = 10;


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
    public AudioClip healthPickupClip;
    public AudioClip ammoPickupClip;
    public AudioClip shotgunPickupClip;

    void Awake()
    {
        //disable cursor
        Cursor.lockState = CursorLockMode.Locked;

        isDead = false;
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        hpText.text = curHp.ToString("D3");
        lookSensitivity = PlayerPrefs.GetFloat("mouseSens");
        //failsafe
        if(lookSensitivity <= 0f)
        {
            lookSensitivity = 1f;
        }
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

    public void RecieveHandgunAmmo(int ammoAmount)
    {
        audioSource.PlayOneShot(ammoPickupClip);
        handgunAmmo += ammoAmount;
        ammoText.text = handgunAmmo.ToString("D3");
    }

    public void RecieveShotgunAmmo(int ammoAmount)
    {
        audioSource.PlayOneShot(ammoPickupClip);
        shotgunAmmo += ammoAmount;
        ammoText.text = shotgunAmmo.ToString("D3");
    }

    public void playShotgunPickupSound()
    {
        audioSource.PlayOneShot(shotgunPickupClip);
    }

}
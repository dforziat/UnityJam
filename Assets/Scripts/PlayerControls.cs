using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    float moveSpeed = 5;

    [Header("Gravity")]
    float grav = -8f;
    [SerializeField] Transform groundCheck;
    float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    private float jumpHeight = 5f;
    public bool isGrappling = false;

    [Header("Camera")]
    public float lookSensitivity;

    [Header("Stats")]
    public int curHp = 90;
    public int maxHp = 100;
    static public bool isDead = false;

    [Header("GUI Ammo Text")]
    public Text hpText;
    public Text handgunAmmoText;
    public Text shotgunAmmoText;
    public Text grapplegunAmmoText;

    [Header("Ammo Count")]
    public int handgunAmmo = 20;
    public int shotgunAmmo = 10;
    public int grapplegunAmmo = 20;

    public int curLevel = 1;

    Vector3 vel;
    [SerializeField] CharacterController controller;

    public Vector3 dir;

    public GameObject hitEffect;
    public Animator gunAnimator;
    public Image damageEffect;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip injuredAudioClip;
    public AudioClip deathAudioClip;
    public AudioClip healthPickupClip;
    public AudioClip ammoPickupClip;

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

        
        lookSensitivity = PlayerPrefs.GetFloat(PlayerPrefsConstants.MOUSE_SENS);
        
        //failsafe
        if(lookSensitivity <= 0f)
        {
            lookSensitivity = 1f;
            PlayerPrefs.SetFloat(PlayerPrefsConstants.MOUSE_SENS, 1);
        }
       
    }

    void Update()
    {
            
        if (isDead || PauseMenu.GameIsPaused || LevelManager.levelLoading)
        {
            return;
        }
        if (!isGrappling)
        {
            Gravity();
            CamLook();
            Movement();
        }
            Grapple();

    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        dir = (transform.right * x + transform.forward * z).normalized;
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

       // if (Input.GetButtonDown("Jump") && isGrounded)
       // {
       //     Jump();
       // }

        vel.y += grav * Time.deltaTime;
        controller.Move(vel * Time.deltaTime);
        if (isGrounded && vel.y < 0)
        {
            vel.y = -2f;
        }
    }

    public void UpdateLookSens()
    {
        lookSensitivity = PlayerPrefs.GetFloat(PlayerPrefsConstants.MOUSE_SENS);
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
        handgunAmmoText.text = handgunAmmo.ToString("D3");
    }

    public void RecieveShotgunAmmo(int ammoAmount)
    {
        audioSource.PlayOneShot(ammoPickupClip);
        shotgunAmmo += ammoAmount;
        shotgunAmmoText.text = shotgunAmmo.ToString("D3");
    }
    public void RecieveGrapplegunAmmo(int ammoAmount)
    {
        audioSource.PlayOneShot(ammoPickupClip);
        grapplegunAmmo += ammoAmount;
        grapplegunAmmoText.text = grapplegunAmmo.ToString("D3");
    }


    public void Jump(float jumpHeight)
    {
        vel.y = Mathf.Sqrt(jumpHeight * -2f * grav);
    }

    public void Grapple()
    {
        if (isGrappling)
        {
            controller.Move(controller.transform.forward * Mathf.Sqrt(1 * Time.deltaTime));
        }
        
    }

}
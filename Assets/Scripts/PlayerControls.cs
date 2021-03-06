using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    [Header("Movement")]
    public static float moveSpeed = 5;
    float grappleSpeed = 100;

    [Header("Gravity")]
    float grav = -10f;
    [SerializeField] Transform groundCheck;
    float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    private float jumpHeight = 5f;

    [Header("GrappleStuff")]
    public bool isGrappling = false;
    Vector3 initialPos;
    float distanceToGrapple;

    [Header("Camera")]
    public float lookSensitivity;

    [Header("Stats")]
    public int curHp = 100;
    public int maxHp = 100;
    public int maxOverHeal = 150;
    static public bool isDead = false;

    [Header("GUI Ammo Text")]
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI handgunAmmoText;
    public TextMeshProUGUI shotgunAmmoText;
    public TextMeshProUGUI machinegunAmmoText;
    //public Text spearAmmoText;


    [Header("Ammo Count")]
    public int handgunAmmo = 25;
    public int shotgunAmmo = 10;
    public int machinegunAmmo = 30;
    //public int spearAmmo = 1;

    public int curLevel;
    Vector3 vel;
    [SerializeField] CharacterController controller;

    public Vector3 dir;

    public GameObject hitEffect;
    public Image damageEffect;
    public GameObject WeaponHolder;

    [Header("Audio")]
    public AudioClip injuredAudioClip;
    public AudioClip injuredAudioClip2;
    public AudioClip deathAudioClip;
    public AudioClip healthPickupClip;
    public AudioClip shieldPickupClip;
    public AudioClip ammoPickupClip;
    private AudioSource audioSource;
    public bool gunSoundOnFirstLoad = true;

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
        if (lookSensitivity <= 0f)
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
            MouseCamLook();
            Movement();
        }
            Grapple();

    }

    private void FixedUpdate()
    {
        if (isDead || PauseMenu.GameIsPaused || LevelManager.levelLoading)
        {
            return;
        }
        if (!isGrappling)
        {
            ControllerCamLook();
        }
    }

    void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        dir = (transform.right * x + transform.forward * z).normalized;
        controller.Move(dir * moveSpeed * Time.deltaTime);

        if(WeaponHolder != null)
        {
            if (dir != null && dir != Vector3.zero)
            {
                WeaponHolder.GetComponent<Animator>().SetBool("sway", true);
            }
            else
            {
                WeaponHolder.GetComponent<Animator>().SetBool("sway", false);
            }
        }
    }

    void MouseCamLook()
    {
        float mousey = Input.GetAxis("Mouse X") * lookSensitivity;
        
        if(mousey != 0)
        {
            transform.eulerAngles += Vector3.up * mousey;
        }
        
    }

    void ControllerCamLook()
    {
        float controllery = Input.GetAxis("RightStick") * lookSensitivity;
        if(controllery != 0)
        {
            transform.eulerAngles += Vector3.up * controllery * 5;
        }
    }

    void Gravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

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

        GetComponent<CameraEffects>().damageShake();

        if (Random.value < 0.5f)
        {
            audioSource.PlayOneShot(injuredAudioClip);
        }
        else
        {
            audioSource.PlayOneShot(injuredAudioClip2);
        }

        curHp -= damage;
        hpText.text = curHp.ToString("D3");

        
        if (curHp <= 100) {
            hpText.color = Color.white;
        }
            
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

    public void OverHeal()
    {
        //this is the shield pickup fully healing and more
        Color steelBlue = new Color32(70, 130, 180, 255);
        curHp = maxOverHeal;
        hpText.text = curHp.ToString("D3");
        hpText.color = steelBlue;
        audioSource.PlayOneShot(shieldPickupClip);
    }

    public void RecieveAmmo()
    {
        audioSource.PlayOneShot(ammoPickupClip);
        handgunAmmo += 15;
        shotgunAmmo += 10;
        machinegunAmmo += 25;
        //spearAmmo += 1;

        updateGUIAmmo();
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
    }


    public void Jump(float jumpHeight)
    {
        vel.y = Mathf.Sqrt(jumpHeight * -2f * grav * Time.deltaTime);
    }

    public void Grapple()
    {
        if (isGrappling)
        {
            if (distanceToGrapple > Vector3.Distance(initialPos, transform.position) + 1)
            {
                Debug.Log("DISTANCE: " + Vector3.Distance(initialPos, transform.position));
                controller.Move(controller.transform.forward * Mathf.Sqrt(grappleSpeed) * Time.deltaTime);
            }
            if (distanceToGrapple <= Vector3.Distance(initialPos, transform.position) + 1)
            {
                stopGrappling();
            }
        }

    }

    public void GrapplePrep(float distanceToGrapplePoint)
    {
        distanceToGrapple = distanceToGrapplePoint;
        initialPos = transform.position;
        isGrappling = true;
    }

    public void updateGUIAmmo()
    {
        if (handgunAmmoText.enabled)
        {
            handgunAmmoText.text = handgunAmmo.ToString("D3");
        }
        if (shotgunAmmoText.enabled)
        {
            shotgunAmmoText.text = shotgunAmmo.ToString("D3");
        }
        if (machinegunAmmoText.enabled)
        {
            machinegunAmmoText.text = machinegunAmmo.ToString("D3");
        }

    }

    private void stopGrappling()
    {
        isGrappling = false;
        if (WeaponHolder.GetComponentInChildren<GrapplegunScript>() != null)
        {
            WeaponHolder.GetComponentInChildren<GrapplegunScript>().ShootingRecovery();
        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            stopGrappling();
        }
    }
}
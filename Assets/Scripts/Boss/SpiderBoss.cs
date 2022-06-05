using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderBoss : BossScript
{

    private float walkSpeed = 1.75f;
    private float runSpeed = 2.25f;
    private float spinSpeed = 4.5f;
    private const int maxHealth = 100;
    private int spinDamage = 20;
    private int stompDamage = 40;
    private float stompRange = 4.5f;
    private float stompProximity = 2f;
    private int attackIndex = 0;
    private const float idleTimerMax = 5f;
    private float idleTimer = 5f;
    private bool isSpinning = false;
    private bool isDead = false;
    private bool isAttacking = false;
    private MeshRenderer[] meshRendererList;
    private List<Color> originalColorList;
    private float flashTime = .2f;


    public Transform head;
    public Transform missileLauncher;
    public GameObject missile;
    public GameObject stompSpriteHolder;
    public ParticleSystem stompParticle;
    public Transform stompLocation;

    public AudioClip spinClip;
    public AudioClip missileLaunchClip;
    public AudioClip transformClip;
    public AudioClip stompExplosionClip;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    public AudioSource walkAudioSource;
    private Transform playerTransform;
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        secondaryAudioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        navMeshAgent.enabled = false;

        meshRendererList = GetComponentsInChildren<MeshRenderer>();
        originalColorList = new List<Color>();
        foreach (MeshRenderer mesh in meshRendererList)
        {
            originalColorList.Add(mesh.material.color);
        }
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
        if (navMeshAgent.enabled)
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }

        //stomp when the player gets too close
        if (Vector3.Distance(playerTransform.position, transform.position) <= stompProximity && animator.GetCurrentAnimatorStateInfo(0).IsName("Boss2_Walking"))
        {
            animator.SetTrigger("stomp");
        }

        idleTimer -= Time.deltaTime;
        // Debug.Log("IdleTimer: " + idleTimer);
        if (idleTimer <= 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Boss2_Walking") && !isAttacking)
        {
            isAttacking = true;
            //increment the attack index
            attackIndex += 1;
            if (attackIndex == 3)
            {
                attackIndex = 1;
            }

            //carry out the attack
            Debug.Log("Attack Index: " + attackIndex);
            switch (attackIndex)
            {
                case 1:
                    animator.SetTrigger("missile");
                    break;
                case 2:
                    animator.SetTrigger("spin");
                    break;
                default:
                    animator.SetTrigger("stomp");
                    break;
            }
        }
        isAttacking = false;
    }

    public new void takeDamage(int damage)
    {

        if (health <= 0 && !isDead)
        {
            isDead = true;
            navMeshAgent.speed = 0;
            animator.SetTrigger("death");
        }
        health -= damage;
        Debug.Log("Boss Health: " + health);

        foreach (MeshRenderer mesh in meshRendererList)
        {
            mesh.material.color = Color.red;

        }
        Invoke("ResetColor", flashTime);

        playDamagedClip();
    }

    private void lookAtPlayer()
    {
        head.LookAt(playerTransform.position);
    }

    private void fireMissile()//Called from animation event
    {
        audioSource.PlayOneShot(missileLaunchClip);
        Instantiate(missile, new Vector3(missileLauncher.transform.position.x, missileLauncher.transform.position.y, missileLauncher.transform.position.z), transform.rotation);
    }

    public void stopWalking()
    {
        // audioSource.PlayOneShot(transformClip);
        navMeshAgent.speed = 0;
        boxCollider.enabled = false;
        walkAudioSource.Stop();
    }

    public void resumeWalking()
    {
        if (!navMeshAgent.enabled)
        {
            navMeshAgent.enabled = true;
        }
        audioSource.Stop();
        isSpinning = false;
        navMeshAgent.speed = walkSpeed;
        if (health <= maxHealth / 2)
        {
            navMeshAgent.speed = runSpeed;
        }
        walkAudioSource.Play();
        animator.ResetTrigger("stomp");
        animator.ResetTrigger("missile");
        animator.ResetTrigger("spin");

    }
    private void playDamagedClip()
    {
        int randNum = Random.Range(1, 4);
        switch (randNum)
        {
            case 1:
                secondaryAudioSource.PlayOneShot(damagedClip);
                break;
            case 2:
                secondaryAudioSource.PlayOneShot(damagedClip2);
                break;
            case 3:
                secondaryAudioSource.PlayOneShot(damagedClip3);
                break;
        }
    }

    public void startSpinning()
    {
        audioSource.PlayOneShot(spinClip);
        navMeshAgent.speed = spinSpeed;
        isSpinning = true;
        boxCollider.enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isSpinning)
            {
                playerTransform.GetComponent<PlayerControls>().TakeDamage(spinDamage);
            }
        }
    }

    public void checkStompDamage()
    {
        if (Vector3.Distance(playerTransform.position, stompLocation.position) <= stompRange)
        {
            playerTransform.GetComponent<PlayerControls>().TakeDamage(stompDamage);
        }
    }

    public void playStompClip()
    {
        audioSource.PlayOneShot(stompExplosionClip);
    }

    public void resetTimer()
    {
        idleTimer = idleTimerMax;
    }


    public void beginFight()
    {
        navMeshAgent.enabled = true;
        animator.SetTrigger("begin");
    }

    public new void die()
    {
        bossDoor.isUnlocked = true;
        Destroy(gameObject);
    }

    private void ResetColor()
    {
        int colorListPlace = 0;
        foreach (MeshRenderer mesh in meshRendererList)
        {
            mesh.material.color = originalColorList[colorListPlace];
            colorListPlace++;
        }
        colorListPlace = 0;
    }
}

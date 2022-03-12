using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderBoss : BossScript
{

    private float walkSpeed = 1f;
    private float spinSpeed = 3f;
    private int maxHealth = 100;
    private int spinDamage = 20;
    private bool isSpinning = false;
    private bool isDead = false;
    private bool fireLeftMissile = true;
 

    public Transform head;
    public Transform missileLauncher;
    public GameObject missile;
    public SpriteRenderer stompOutline;
    public ParticleSystem stompParticle;

    public AudioClip spinClip;
    public AudioClip missileLaunchClip;
    public AudioClip transformClip;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    private Transform playerTransform;
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        health = 99;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        secondaryAudioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        stompOutline.enabled = false;
        stompParticle.enableEmission = false;
        animator.SetTrigger("missile");
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
        navMeshAgent.SetDestination(playerTransform.position);
    }

    public new void takeDamage(int damage)
    {

        if (health <= 0 && !isDead)
        {
            isDead = true;
            animator.SetTrigger("death");
        }
        health -= damage;
        Debug.Log("Boss Health: " + health);

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
    }

    public void resumeWalking()
    {
        navMeshAgent.speed = walkSpeed;
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
        if(other.gameObject.tag == "Player" && isSpinning)
        {
            playerTransform.GetComponent<PlayerControls>().TakeDamage(spinDamage);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss3Script : MonoBehaviour
{
    // Start is called before the first frame update

    public int health;
    private const float speed = 3.5f;

    private bool isDead = false;

    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;

    private string state = "";
    private const string RUN_TO_POINT = "runToPoint";
    private const string SHOOT = "shoot";
    private const string WAIT = "wait";
    private const string PROCESSING = "processing";


    public Transform[] firstRoomPoints;
    private int firstRoomCounter = 0;

    public int saberDamage = 15;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip[] damagedClips;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        state = RUN_TO_POINT;
        processLogic();
    }

    // Update is called once per frame
    void Update()
    {
        checkWalkAnimation();
    }

    public void takeDamage(int damage)
    {

        if (health <= 0 && !isDead)
        {
            isDead = true;
            navMeshAgent.speed = 0;
            animator.SetTrigger("death");
        }
        health -= damage;
        Debug.Log("Boss Health: " + health);
        playDamagedClip();

    }


    private void playDamagedClip()
    {
        int randNum = Random.Range(0, damagedClips.Length);
        audioSource.PlayOneShot(damagedClips[randNum]);
    }

    private void checkWalkAnimation()
    {
        if (navMeshAgent.velocity.magnitude > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
    }

    public void runToPoint()
    {
        if(firstRoomCounter > firstRoomPoints.Length)
        {
            firstRoomCounter = 0;
        }
        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(firstRoomPoints[firstRoomCounter].position);
        firstRoomCounter++;

    }

    public void shoot()
    {
        animator.SetBool("shoot", true);
    }

    public void wait()
    {
        navMeshAgent.speed = 0;
        navMeshAgent.SetDestination(playerTransform.position);
    }

    public void processLogic()
    {
        //first room logic
        Debug.Log("Process Logic State: " + state);
        switch (state)
        {
            case RUN_TO_POINT:
                runToPoint();
                break;
            case SHOOT:
                shoot();
                break;
            case WAIT:
                wait();
                break;
            default:
                wait();
                break;
        }
    }
}

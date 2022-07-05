using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss3Script : MonoBehaviour
{
    // Start is called before the first frame update

    public int health;
    private const int  maxHealth = 300;
    private const float speed = 3.5f;

    private int bossStage = 1;

    private bool isDead = false;

    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    public Transform destinationTarget;

    private string state = "";
    private const string RUN_TO_POINT = "runToPoint";
    private const string SHOOT = "shoot";
    private const string WAIT = "wait";
    private const string GRENADE = "grenade";
    private const string SWORD_SWING = "swordswing";
    private const string SWORD_JUMP = "swordjump";
    private const string ANIMATION = "animation";


    public Transform[] firstRoomPoints;
    private int firstRoomCounter = 0;

    public Transform grenadePoint;

    public int saberDamage = 15;
    private int burstCount = 0;
    private const float rifleCooldown = 1f;
    private float rifleCooldowntimer = rifleCooldown;

    private int damageThreshold = 10;
    private int currentThreshold = 0;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip[] damagedClips;


    void Start()
    {
        health = maxHealth;
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
        processStageOne();
        processStageTwo();

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


        //go to stage 2 once the boss is at 2/3 health. 
        if (health <= 290 && bossStage == 1)
        {
            Debug.Log("Boss Stage 2");
            bossStage = 2;
            moveToSecondRoom();
        }

        currentThreshold += damage;
        if(bossStage == 1)
        {
            if (currentThreshold > damageThreshold)
            {
                state = RUN_TO_POINT;
                processLogic();
                currentThreshold = 0;
            }
        }

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
        if(firstRoomCounter > firstRoomPoints.Length - 1)
        {
            firstRoomCounter = 0;
        }
        navMeshAgent.speed = speed;
        destinationTarget = firstRoomPoints[firstRoomCounter];
        navMeshAgent.SetDestination(destinationTarget.position);
        firstRoomCounter++;
    }

    public void shoot()
    {
        if(burstCount < 1)
        {
            animator.SetTrigger("shoot");
            burstCount++;
        }
        else
        {
            //start cool down timer
            if (rifleCooldowntimer > 0)
            {
                rifleCooldowntimer -= Time.deltaTime;
            }
            else
            {
                burstCount = 0;
                rifleCooldowntimer = rifleCooldown;
                if(bossStage == 2)
                {
                    chooseRandomAttack();
                }
            }

        }
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

    public void rotateTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2);
    }

    public bool canSeePlayer()
    {
        Physics.Linecast(transform.position, playerTransform.position, out RaycastHit hitInfo);
        if (hitInfo.transform != null && hitInfo.transform.CompareTag("Player"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void processStageOne()
    {
        if (bossStage == 1)
        {
            if (state == RUN_TO_POINT)
            {
                if (gameObject.transform.position.x == destinationTarget.position.x && gameObject.transform.position.z == destinationTarget.position.z)
                {
                    state = WAIT;
                    processLogic();
                }
            }

            if (state == WAIT)
            {
                rotateTowardsPlayer();
                //look for player to shoot
                if (canSeePlayer())
                {
                    shoot();
                }
            }
        }
    }



    private void processStageTwo()
    {
        if(bossStage == 2)
        {
            if (state == GRENADE)
            {
                if (gameObject.transform.position.x == grenadePoint.position.x && gameObject.transform.position.z == grenadePoint.position.z)
                {
                    animator.SetTrigger("grenade");
                    state = ANIMATION;
                }
            }

            if (state == SWORD_SWING)
            {
                if (Vector3.Distance(gameObject.transform.position, playerTransform.position) <= 1)
                {
                    animator.SetTrigger("saber");
                    state = ANIMATION;
                }
            }

            if (state == SWORD_JUMP)
            {
                if (Vector3.Distance(gameObject.transform.position, playerTransform.position) <= 2)
                {
                    animator.SetTrigger("saberjump");
                    state = ANIMATION;
                }
            }

            if (state == SHOOT)
            {
                if (gameObject.transform.position.x == grenadePoint.position.x && gameObject.transform.position.z == grenadePoint.position.z)
                {
                    animator.SetTrigger("drawrifle");
                    shoot();
                    rotateTowardsPlayer();
                    state = ANIMATION;
                }
            }

            if(state == WAIT)
            {
                rotateTowardsPlayer();
            }
        }
    }

    public void chooseRandomAttack()//call from animation events
    {
        if(bossStage == 2)
        {
            int randomNum = Random.Range(0, 1);

            switch (randomNum)
            {
                case 0:
                    moveToGrenadePoint();
                    break;
                case 1:
                    moveToShootPoint();
                    break;
                case 2:
                    swordSwingAttack();
                    break;
                case 3:
                    swordJumpAttack();
                    break;
            }
        }
    }

    public void swordSwingAttack()
    {
        navMeshAgent.speed = speed;
        animator.SetTrigger("draw");
        navMeshAgent.SetDestination(playerTransform.position);
        state = SWORD_SWING;
    }

    public void swordJumpAttack()
    {
        navMeshAgent.speed = speed;
        animator.SetTrigger("draw");
        navMeshAgent.SetDestination(playerTransform.position);
        state = SWORD_JUMP;
    }

    public void moveToGrenadePoint()
    {
        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(grenadePoint.position);
        state = GRENADE;
    }

    public void moveToShootPoint()
    {
        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(grenadePoint.position);
        state = SHOOT;
    }

    public void moveToSecondRoom()
    {
        navMeshAgent.speed = speed;
        navMeshAgent.SetDestination(grenadePoint.position);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss3Script : MonoBehaviour
{
    // Start is called before the first frame update

    public int health;
    private const int maxHealth = 300;
    private const float speed = 3.5f;
    private const float runSpeed = 6f;

    private int bossStage = 1;

    private bool isDead = false;
    private bool rotateTowardsPlayerBool = false;
    public bool swordIsDrawn = false;

    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    public Transform destinationTarget;

    public string state = "";
    private const string RUN_TO_POINT = "runToPoint";
    private const string SHOOT = "shoot";
    private const string WAIT = "wait";
    private const string GRENADE = "grenade";
    private const string SWORD_SWING = "swordswing";
    private const string SWORD_JUMP = "swordjump";
    private const string ANIMATION = "animation";
    private const string MOVING = "moving";
    public const string SNIPE = "snipe";




    public Transform[] firstRoomPoints;
    private int firstRoomCounter = 0;

    public DoorProximityScript secondRoomDoor;
    public DoorProximityScript sniperRoomDoor;



    public Transform grenadePoint;
    public Transform sniperPoint;

    public int saberDamage = 15;
    public int sniperDamage = 40;
    private const float sniperAimTime = 5f;
    private float sniperAimTimer = sniperAimTime;
    private const float sniperCoolDownTimer = 5f;
    private float sniperCoolDownTime = sniperCoolDownTimer;

    private int burstCount = 0;
    private const float rifleCooldown = 1f;
    private float rifleCooldowntimer = rifleCooldown;
    public LineRenderer sniperLaser;
    public Transform muzzleTransform;

    private int damageThreshold = 10;
    private int currentThreshold = 0;

    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip[] damagedClips;
    public AudioClip sniperShotClip;


    void Start()
    {
        health = maxHealth;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        state = RUN_TO_POINT;
        processLogic();
        sniperLaser.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkWalkAnimation();
        processStageOne();
        processStageTwo();
        processStageThree();
        if (rotateTowardsPlayerBool)
        {
            rotateTowardsPlayer();
        }

   
        sniperLaser.SetPosition(0, muzzleTransform.position);
        sniperLaser.SetPosition(1, new Vector3(playerTransform.position.x, playerTransform.position.y - .5f, playerTransform.position.z));

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
        // Debug.Log("Boss Health: " + health);
        playDamagedClip();


        //go to stage 2 once the boss is at 2/3 health. 
        if (health <= 290 && bossStage == 1)
        {
            animator.ResetTrigger("shoot");
            Debug.Log("Boss Stage 2");
            bossStage = 2;
            moveToSecondRoom();
            secondRoomDoor.isUnlocked = true;
        }

        if (health <= 280 && bossStage == 2)
        {
            animator.ResetTrigger("shoot");
            animator.SetTrigger("drawrifle");
            StartCoroutine(pauseMovement());
            Debug.Log("Boss Stage 3");
            bossStage = 3;
            moveToSniperPoint();
            sniperRoomDoor.isUnlocked = true;
        }


        currentThreshold += damage;
        if (bossStage == 1)
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
        if (firstRoomCounter > firstRoomPoints.Length - 1)
        {
            firstRoomCounter = 0;
        }
        destinationTarget = firstRoomPoints[firstRoomCounter];
        navMeshAgent.SetDestination(destinationTarget.position);
        firstRoomCounter++;
    }

    public void shoot()
    {
        animator.SetTrigger("shoot");
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
                break;
            default:
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
        if (bossStage == 2 && state != MOVING)
        {
            if (state == GRENADE)
            {
                rotateTowardsPlayerBool = true;
                navMeshAgent.SetDestination(grenadePoint.position);
                if (gameObject.transform.position.x == grenadePoint.position.x && gameObject.transform.position.z == grenadePoint.position.z)
                {
                    animator.SetTrigger("grenade");
                    state = ANIMATION;
                }
            }

            if (state == SWORD_SWING)
            {
                navMeshAgent.SetDestination(playerTransform.position);
                if (Vector3.Distance(gameObject.transform.position, playerTransform.position) <= 2)
                {
                    animator.SetTrigger("saber");
                    state = ANIMATION;
                }
            }

            if (state == SWORD_JUMP)
            {
                navMeshAgent.SetDestination(playerTransform.position);
                if (Vector3.Distance(gameObject.transform.position, playerTransform.position) <= 2)
                {
                    animator.SetTrigger("saberjump");
                    state = ANIMATION;
                }
            }

            if (state == SHOOT)
            {
                navMeshAgent.SetDestination(grenadePoint.position);
                if (gameObject.transform.position.x == grenadePoint.position.x && gameObject.transform.position.z == grenadePoint.position.z)
                {
                    rotateTowardsPlayerBool = true;
                    animator.SetTrigger("drawrifle");
                    StartCoroutine(pauseMovement());
                    shoot();
                    state = ANIMATION;
                }
            }

            if (state == WAIT)
            {
                rotateTowardsPlayer();
            }
        }

        if (bossStage == 2 && state == MOVING)
        {
            if (gameObject.transform.position.x == grenadePoint.position.x && gameObject.transform.position.z == grenadePoint.position.z)
            {
                state = WAIT;
            }
        }
    }

    public void chooseRandomAttack()//call from animation events
    {
        if (bossStage == 2 && state != MOVING)
        {
            rotateTowardsPlayerBool = false;
            int randomNum = Random.Range(0, 3);

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
        animator.ResetTrigger("drawrifle");
        animator.SetTrigger("draw");
        StartCoroutine(pauseMovement());
        state = SWORD_SWING;
    }

    public void swordJumpAttack()
    {
        animator.ResetTrigger("drawrifle");
        animator.SetTrigger("draw");
        StartCoroutine(pauseMovement());
        state = SWORD_JUMP;
    }

    public void moveToGrenadePoint()
    {
        animator.ResetTrigger("draw");
        animator.SetTrigger("drawrifle");
        StartCoroutine(pauseMovement());
        state = GRENADE;
    }

    public void moveToShootPoint()
    {
        state = SHOOT;
    }

    public void moveToSecondRoom()
    {
        navMeshAgent.SetDestination(grenadePoint.position);
        state = MOVING;
    }


    private IEnumerator pauseMovement()
    {
        navMeshAgent.speed = 0;
        yield return new WaitForSeconds(1.5f);
        navMeshAgent.speed = speed;
    }

    private void moveToSniperPoint()
    {
        navMeshAgent.SetDestination(sniperPoint.position);
        state = MOVING;
    }

    private void processStageThree()
    {
        if (bossStage == 3 && state != MOVING)
        {
            if(state == SNIPE)
            {
                aimSniper();
                sniperShoot();
            }

        }

        if (bossStage == 3 && state == MOVING)
        {
            if (gameObject.transform.position.x == sniperPoint.position.x && gameObject.transform.position.z == sniperPoint.position.z)
            {
                rotateTowardsPlayerBool = true;
                state = WAIT;
            }
        }
    }

    public void aimSniper()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        LayerMask enemyLayerMask = LayerMask.GetMask("Wall");
        sniperLaser.enabled = true;
        /*  if (Physics.Raycast(muzzleTransform.position, new Vector3(playerTransform.position.x, playerTransform.position.y - .5f, playerTransform.position.z), out hit, Mathf.Infinity, enemyLayerMask))
          {
              if(hit.collider.tag == "Player")
              {
                  sniperLaser.enabled = true;
              }

          }
          else
          {

              sniperLaser.enabled = false;
          }
          */
    }

    public void sniperShoot()
    {
        if(sniperAimTimer <= 0)
        {
            shootSniper();
            StartCoroutine(sniperCooldown());
        }
        else
        {
            sniperAimTimer -= Time.deltaTime;
        }
    }

    public void shootSniper()
    {
        sniperLaser.enabled = false ;

        audioSource.PlayOneShot(sniperShotClip);
        RaycastHit hit;
        LayerMask enemyLayerMask = LayerMask.GetMask("Wall");
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(muzzleTransform.position, playerTransform.position, out hit, Mathf.Infinity, enemyLayerMask))
        {
            if(hit.collider.tag == "Player")
            {
                hit.transform.GetComponent<PlayerControls>().TakeDamage(sniperDamage);
            }
            else
            {
                Debug.Log("Sniper is hitting: " + hit.collider.name);
            }
        }

    }

    public IEnumerator sniperCooldown()
    {
        state = WAIT;
        yield return new WaitForSeconds(sniperCoolDownTime);
        state = SNIPE;
        sniperAimTimer = sniperAimTime;
    }
}

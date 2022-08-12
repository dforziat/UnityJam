using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss3Script : MonoBehaviour
{
    // Start is called before the first frame update

    public int health;
    private const int maxHealth = 170;
    private const float speed = 3.5f;
    private const float runSpeed = 5f;
    private const float meleeRange = 4f;

    private int bossStage = 1;

    private bool isDead = false;
    private bool isCutscene = true;
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
    public DoorProximityScript finalDoor;

    public Transform grenadePoint;
    public Transform sniperPoint;

    public int saberDamage = 15;
    public int sniperDamage = 40;
    private const float sniperAimTime = 5f;
    private float sniperAimTimer = sniperAimTime;
    private const float sniperCoolDownTimer = 5f;
    private float sniperCoolDownTime = sniperCoolDownTimer;

    private const float rifleCooldown = 1f;
    public LineRenderer sniperLaser;
    public Transform muzzleTransform;

    private int damageThreshold = 10;
    private int currentThreshold = 0;

    private SkinnedMeshRenderer meshRenderer;
    private List<Color> originalColorList;
    private float flashTime = .2f;


    [Header("Audio")]
    private AudioSource audioSource;
    public AudioSource runningAudioSource;
    public AudioClip[] damagedClips;
    public AudioClip sniperShotClip;


    void Start()
    {
        health = maxHealth;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        state = RUN_TO_POINT;
       // processLogic();
        sniperLaser.enabled = false;
        originalColorList = new List<Color>();
        foreach (Material material in meshRenderer.materials)
        {
            originalColorList.Add(material.color);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || isCutscene)
        {
            return;
        }
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
            animator.CrossFade("Boss3_Death", .5f);
            finalDoor.isUnlocked = true;
        }
        health -= damage;
        // Debug.Log("Boss Health: " + health);
        playDamagedClip();


        //go to stage 2 once the boss is at 2/3 health. 
        if (health <= 120 && bossStage == 1)
        {
            animator.ResetTrigger("shoot");
            StartCoroutine(pauseMovement());
            Debug.Log("Boss Stage 2");
            bossStage = 2;
            moveToSecondRoom();
            secondRoomDoor.isUnlocked = true;
        }

        if (health <= 20 && bossStage == 2)
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

        foreach (Material material in meshRenderer.materials)
        {
            material.color = Color.red;

        }
        Invoke("ResetColor", flashTime);

    }


    private void playDamagedClip()
    {
        int randNum = Random.Range(0, damagedClips.Length);
        audioSource.PlayOneShot(damagedClips[randNum]);
    }

    private void checkWalkAnimation()
    {
        if(animator != null)
        {
            if (navMeshAgent.velocity.magnitude > 0)
            {
                animator.SetBool("run", true);
                if (!runningAudioSource.isPlaying)
                {
                  runningAudioSource.Play();
                }
                
            }
            else
            {
                animator.SetBool("run", false);
                runningAudioSource.Stop();
            }
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
                if (Vector3.Distance(gameObject.transform.position, playerTransform.position) <= meleeRange) //&& meleeCollsionCheck())
                {
                    Debug.Log("Melee Distance");
                    rotateTowardsPlayer();
                    animator.SetTrigger("saber");
                    state = ANIMATION;
                }
                else
                {
                    navMeshAgent.SetDestination(playerTransform.position);
                }
            }

            if (state == SWORD_JUMP)
            {
                
                if (Vector3.Distance(gameObject.transform.position, playerTransform.position) <= meleeRange)// && meleeCollsionCheck())
                {
                    Debug.Log("Melee Distance");
                    rotateTowardsPlayer();
                    animator.SetTrigger("saberjump");
                    state = ANIMATION;
                }
                else
                {
                    navMeshAgent.SetDestination(playerTransform.position);
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
            navMeshAgent.SetDestination(grenadePoint.position);
            navMeshAgent.speed = runSpeed;
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
            int randomNum = Random.Range(0, 4);

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

    private IEnumerator microPause()
    {
        navMeshAgent.speed = 0;
        yield return new WaitForSeconds(.3f);
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
            navMeshAgent.speed = runSpeed;
            navMeshAgent.SetDestination(sniperPoint.position);
            rotateTowardsPlayerBool = false;
            if (gameObject.transform.position.x == sniperPoint.position.x && gameObject.transform.position.z == sniperPoint.position.z)
            {
                animator.ResetTrigger("draw");
                animator.SetTrigger("drawrifle");
                animator.CrossFade("Boss3_Idle", .3f);
                StartCoroutine(pauseMovement());
                rotateTowardsPlayerBool = true;
                state = WAIT;
            }
        }
    }

    public void aimSniper()
    {
        LayerMask enemyLayerMask = LayerMask.GetMask("Enemy");
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Linecast(muzzleTransform.position, playerTransform.position, out RaycastHit hitInfo, ~enemyLayerMask))
        {
            if (hitInfo.collider.tag == "Player")
            {
                sniperLaser.enabled = true;
            }
            else
            {
                sniperLaser.enabled = false;
            }
        }
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
        sniperLaser.enabled = false;

        audioSource.PlayOneShot(sniperShotClip);
        LayerMask enemyLayerMask = LayerMask.GetMask("Enemy");
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Linecast(muzzleTransform.position, playerTransform.position, out RaycastHit hitInfo, ~enemyLayerMask))
        {
            Debug.Log("Linecast hit: " + hitInfo.collider.name);
            if(hitInfo.collider.tag == "Player")
            {
                playerTransform.GetComponent<PlayerControls>().TakeDamage(sniperDamage);
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

    private void ResetColor()
    {
        int colorListPlace = 0;
        foreach (Material material in meshRenderer.materials)
        {
            material.color = originalColorList[colorListPlace];
            colorListPlace++;
        }
        colorListPlace = 0;
    }

    public void startFight()
    {
        isCutscene = false;
        animator.SetTrigger("activate");
        processLogic();
    }

}

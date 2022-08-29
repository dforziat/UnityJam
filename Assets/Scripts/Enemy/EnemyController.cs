using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyParent
{
    private NavMeshAgent navMeshAgent;
    private AudioSource childAudioSource;
    private float chaseRange = 5;
    private float distanceToTarget = Mathf.Infinity;
    public GameObject[] EnemyList;
    private Animator animator;
    private float cVerticalOffset = 0f;

    public Sprite damagedSprite;
    public Sprite idleSprite;

    public AudioClip provokedClip;
    public AudioClip attackClip;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        childAudioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        target = GameObject.FindGameObjectWithTag("Player").transform;
        


        //set parent vars
        audioSource = childAudioSource;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (canSeePlayer())
        {
            isProvoked = true;
            childAudioSource.PlayOneShot(provokedClip);
        }
    }

    private void FixedUpdate()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
    }

    private void EngageTarget()
    {
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        animator.SetBool("attack", false);
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        animator.SetBool("attack", true);
    }

    public void AttackHitEvent()//have animation call this method
    {
        if (target == null)
        {
            return;
        }
        childAudioSource.PlayOneShot(attackClip);
        target.GetComponent<PlayerControls>().TakeDamage(damage);
    }

    new public void takeDamage(int damage)
    {
        isProvoked = true;
        health -= damage;
        StartCoroutine(FlashRed());

        //target.GetComponent<CameraEffects>().Shake();

        if (health <= 0)
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - cVerticalOffset, transform.position.z), transform.rotation);
            dropItem();
            Destroy(gameObject);

            //Add speedStack - Functionaility
            //SpeedStack speedStack = GameObject.FindGameObjectWithTag("Player").GetComponent<SpeedStack>();
            //speedStack.stackAdd();
        }
        if (audioSource.enabled == true)
        {
            audioSource.PlayOneShot(damagedClip);
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.sprite = damagedSprite;
        yield return new WaitForSeconds(.15f);
        spriteRenderer.sprite = idleSprite;
    }


    public void BroadcastAggroMessageToOtherEnemies()
    {
        foreach (GameObject enemy in EnemyList)
        {
            if (enemy != null)
            {
                enemy.BroadcastMessage("aggroToPlayer", transform);
            }
        }
    }

    public void aggroToPlayer(Transform broadcastedEnemy)
    {
        if (Vector3.Distance(broadcastedEnemy.position, transform.position) <= chaseRange)
        {
            isProvoked = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SlimeScript : EnemyParent
{
    public GameObject childExplosion;
    private NavMeshAgent navMeshAgent;
    private float chaseRange = 5;
    private float distanceToTarget = Mathf.Infinity;
    private float verticalOffset = 0;
    private Animator animator;
    private bool isBaby = false;
    public GameObject baby; //this is just the slime prefab lol

    private AudioSource childAudioSource;
    public AudioClip childDamagedClip;
    public AudioClip attackClip;
    public AudioClip provokedClip;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        childAudioSource = GetComponent<AudioSource>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //set parent vars
        explosion = childExplosion;
        audioSource = childAudioSource;
        damagedClip = childDamagedClip;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //slime stats
        health = 6;
        damage = 15;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

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
        audioSource.PlayOneShot(attackClip);
        target.GetComponent<PlayerControls>().TakeDamage(damage);
    }

    public void makeBabiesOnDeath()
    { 
        if (!isBaby)
        {
            Debug.Log("Making babies");
            //make 2 small babies
            GameObject leftBaby = Instantiate(baby, new Vector3(transform.position.x, transform.position.y, transform.position.z - .5f), transform.rotation);
            leftBaby.transform.localScale -= new Vector3(.3f, .3f,.3f);
            leftBaby.GetComponent<NavMeshAgent>().speed = 3;
            leftBaby.GetComponent<SlimeScript>().health = 2;
            leftBaby.GetComponent<SlimeScript>().isBaby = true;

            GameObject rightBaby = Instantiate(baby, new Vector3(transform.position.x, transform.position.y, transform.position.z + .5f), transform.rotation);
            rightBaby.transform.localScale -= new Vector3(.3f, .3f, .3f);
            rightBaby.GetComponent<NavMeshAgent>().speed = 3;
            rightBaby.GetComponent<SlimeScript>().health = 2;
            rightBaby.GetComponent<SlimeScript>().isBaby = true;

            isBaby = true;
        }
    }


    public new void takeDamage(int damage)
    {
        isProvoked = true;
        health -= damage;
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - verticalOffset, transform.position.z), transform.rotation);
            dropItem();

            makeBabiesOnDeath();

            Destroy(gameObject);
        }
        if (audioSource.enabled == true)
        {
            audioSource.PlayOneShot(damagedClip);
        }
    }

}

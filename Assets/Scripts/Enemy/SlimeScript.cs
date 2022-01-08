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
        //health = 6;
        damage = 15;
        spriteRenderer.color = Color.white;

        Debug.Log("Slime is born");
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

    public void makeBabiesOnDeath()
    {
        if (!isBaby)
        {
            Debug.Log("Making babies");
            //make 2 small babies
            GameObject leftBaby = Instantiate(baby, new Vector3(transform.position.x, transform.position.y, transform.position.z - .75f), transform.rotation);
            setBabyStats(leftBaby);


            GameObject rightBaby = Instantiate(baby, new Vector3(transform.position.x, transform.position.y, transform.position.z + .75f), transform.rotation);
            setBabyStats(rightBaby);

            isBaby = true;
        }
    }

    public void setBabyStats(GameObject baby)
    {
        Vector3 babyScaleVector = new Vector3(.2f, .2f, .2f);
        baby.transform.localScale -= babyScaleVector;
        baby.GetComponent<NavMeshAgent>().speed = 3;
        baby.GetComponent<SlimeScript>().health = 2;
        baby.GetComponent<SlimeScript>().isBaby = true;
        Debug.Log("Baby Health: " + baby.GetComponent<SlimeScript>().health);
    }

}

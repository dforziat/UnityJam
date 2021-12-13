using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SuicideBugScript : EnemyParent
{
    public GameObject childExplosion;
    private NavMeshAgent navMeshAgent;
    private float chaseRange = 5;
    private float distanceToTarget = Mathf.Infinity;
    private float verticalOffset = 0;
    private Animator animator;
    private int explosionDamage = 20;

    private AudioSource childAudioSource;
    public AudioClip childDamagedClip;
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
        health = 1;
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
            explode();
        }


    }

    private void ChaseTarget()
    {
        navMeshAgent.SetDestination(target.position);
    }


    public void explode()//have animation call this method
    {
        if (target == null)
        {
            return;
        }
        var explosionVar = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - verticalOffset, transform.position.z), transform.rotation);
        explosionVar.GetComponent<SpriteRenderer>().color = Color.green;

        dropItem();
        if(distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            target.GetComponent<PlayerControls>().TakeDamage(explosionDamage);
        }
        Destroy(gameObject);
    }




    public new void takeDamage(int damage)
    {
        isProvoked = true;
        health -= damage;
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            explode();
        }
        if (audioSource.enabled == true)
        {
            audioSource.PlayOneShot(damagedClip);
        }
    }

}

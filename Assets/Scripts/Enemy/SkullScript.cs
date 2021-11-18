using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SkullScript : EnemyParent
{
    public GameObject childExplosion;
    private NavMeshAgent navMeshAgent;
    private float chaseRange = 5;
    private float distanceToTarget = Mathf.Infinity;
    public GameObject fireball;
    private float fireballSpeed = 5f;
    private float yOffset = -0.1f;
    private Animator animator;


    private AudioSource childAudioSource;
    public AudioClip childDamagedClip;
    public AudioClip fireballClip;
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
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        if (!animator.GetBool("shoot"))
        {
            animator.SetBool("shoot", true);
        }
    }

    public void ShootFireEvent()//have animation call this method
    {
        Debug.Log("Shoot Event");
        childAudioSource.PlayOneShot(fireballClip);
        GameObject shootingBullet = Instantiate(fireball);
        shootingBullet.transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        shootingBullet.transform.LookAt(target);
        shootingBullet.GetComponent<Rigidbody>().velocity = shootingBullet.transform.forward.normalized * fireballSpeed;
    }
    
    public void ShootRecoveryEvent()//have animation call this method
    {
        animator.SetBool("shoot", false);
    }

}

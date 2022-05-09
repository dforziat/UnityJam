using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GolemScript : EnemyParent
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
        Vector3 startPos = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        childAudioSource.PlayOneShot(fireballClip);
        GameObject shootingBullet = Instantiate(fireball);
        GameObject leftShootingBullet = Instantiate(fireball);
        GameObject rightShootingBullet = Instantiate(fireball);

        leftShootingBullet.transform.position = startPos;
        rightShootingBullet.transform.position = startPos;
        leftShootingBullet.transform.LookAt(new Vector3(target.position.x - 1f, target.position.y, target.position.z));
        rightShootingBullet.transform.LookAt(new Vector3(target.position.x + 1f, target.position.y, target.position.z));


        leftShootingBullet.GetComponent<Rigidbody>().velocity = leftShootingBullet.transform.forward.normalized * fireballSpeed;
        rightShootingBullet.GetComponent<Rigidbody>().velocity = rightShootingBullet.transform.forward.normalized * fireballSpeed;
        shootingBullet.transform.position = startPos;
        shootingBullet.transform.LookAt(target);
        shootingBullet.GetComponent<Rigidbody>().velocity = shootingBullet.transform.forward.normalized * fireballSpeed;
        Debug.Log("Shoot Event");
    }

    public void ShootRecoveryEvent()//have animation call this method
    {
        animator.SetBool("shoot", false);
    }

}

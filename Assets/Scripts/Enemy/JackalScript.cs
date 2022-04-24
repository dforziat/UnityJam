using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class JackalScript : EnemyParent
{
    public GameObject childExplosion;
    private NavMeshAgent navMeshAgent;
    private float chaseRange = 5;
    private float stoppingDistanceToPlayer = 4;
    private float fleeDistance = 3f;
    private float distanceToTarget = Mathf.Infinity;
    public GameObject fireball;
    private float fireballSpeed = 5f;
    private float yOffset = -0.1f;
    private Animator animator;
    public GameObject shield;

    private AudioSource childAudioSource;
    public AudioClip childDamagedClip;
    public AudioClip fireballClip;
    public AudioClip provokedClip;
    public AudioClip shieldClip;
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
       if (distanceToTarget >= stoppingDistanceToPlayer && !animator.GetBool("shoot"))
        {
            shield.SetActive(true);
            navMeshAgent.isStopped = false;
            animator.SetBool("forward", true);
            ChaseTarget();
        }
        if (distanceToTarget <= stoppingDistanceToPlayer && distanceToTarget > fleeDistance)//5f
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("backward", false);
            animator.SetBool("forward", false);
            //attackOrBlock();
            AttackTarget();
        } 
        if(distanceToTarget <= fleeDistance && !animator.GetBool("shoot"))//2.5f
        {
            shield.SetActive(true);
            navMeshAgent.isStopped = false;
            Vector3 runTo = transform.position + ((transform.position - target.position) * 2);
            navMeshAgent.SetDestination(runTo);
            animator.SetBool("backward", true);
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
            shield.SetActive(false);
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


    public new void takeDamage(int damage)
    {
        if (shield.activeSelf)
        {
            audioSource.PlayOneShot(shieldClip);
            return;
        }
        isProvoked = true;
        health -= damage;
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z), transform.rotation);
            dropItem();

            Destroy(gameObject);
        }
        if (audioSource.enabled == true)
        {
            audioSource.PlayOneShot(damagedClip);
        }
    }

    public void attackOrBlock() //call at the end of animation too
    {
        float randomNum = Random.value;
        if(randomNum > .5)
        {
            AttackTarget();
        }
        else
        {
            shieldBlock();
        }
        
    }

    public void shieldBlock() // go back to idle state
    {

    }
}

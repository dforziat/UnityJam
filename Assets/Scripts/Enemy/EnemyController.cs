using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyParent
{
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private AudioSource childAudioSource;
    private float chaseRange = 5;
    private float distanceToTarget = Mathf.Infinity;
    public GameObject[] EnemyList;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        childAudioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerControls>().transform;
        EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
        //set parent vars
        audioSource = childAudioSource;
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if(canSeePlayer())
        {
            isProvoked = true;
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
        GetComponent<Animator>().SetBool("attack", false);
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        GetComponent<Animator>().SetBool("attack", true);
    }

    public void AttackHitEvent()//have animation call this method
    {
        if(target == null)
        {
            return;
        }
        target.GetComponent<PlayerControls>().TakeDamage(damage);
    }


    public bool canSeePlayer()
    {
        NavMeshHit navMeshHit;
        if (!navMeshAgent.Raycast(target.position, out navMeshHit))
        {
            return true;
        }
        else
        {
            return false;
        }
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
        if(Vector3.Distance(broadcastedEnemy.position, transform.position) <= chaseRange)
        {
            isProvoked = true;
        }
    }
}

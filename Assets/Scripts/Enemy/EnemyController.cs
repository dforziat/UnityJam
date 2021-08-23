using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int health = 3;
    public int damage = 10;
    public GameObject explosion;
    private float verticalOffset = 0;
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private float chaseRange = 5;
    private float distanceToTarget = Mathf.Infinity;
    private bool isProvoked = false;
    private AudioSource audioSource;
    public AudioClip damagedClip;
    public GameObject[] EnemyList;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        target = FindObjectOfType<PlayerControls>().transform;
        EnemyList = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange || canSeePlayer())
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

    public void takeDamage(int damage)
    {
        isProvoked = true;
        BroadcastAggroMessageToOtherEnemies();
        health -= damage;
        GetComponent<Animator>().SetTrigger("damaged");
        
        if (health <= 0) {
            Instantiate(explosion, new Vector3(transform.position.x,transform.position.y - verticalOffset,transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
        if(audioSource.enabled == true)
        {
            audioSource.PlayOneShot(damagedClip);
        }
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

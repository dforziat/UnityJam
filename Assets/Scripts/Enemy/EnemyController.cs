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
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        target = FindObjectOfType<PlayerControls>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (isProvoked)
        {
            Debug.Log("Is Provoked");
            EngageTarget();
        }
        else if(distanceToTarget <= chaseRange)
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
        Debug.Log("Is Chasing Target");
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
}

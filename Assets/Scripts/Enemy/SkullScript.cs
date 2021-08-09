using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SkullScript : MonoBehaviour
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
    public GameObject fireball;
    private float fireballSpeed = 5f;
    private float yOffset = -0.1f;
    private Animator animator;


    private AudioSource audioSource;
    public AudioClip damagedClip;
    public AudioClip fireballClip;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
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
        audioSource.PlayOneShot(fireballClip);
        GameObject shootingBullet = Instantiate(fireball);
        shootingBullet.transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        shootingBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * fireballSpeed;
    }
    
    public void ShootRecoveryEvent()//have animation call this method
    {
        animator.SetBool("shoot", false);
    }

    public void takeDamage(int damage)
    {
        isProvoked = true;
        health -= damage;
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - verticalOffset, transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
        if (audioSource.enabled == true)
        {
            audioSource.PlayOneShot(damagedClip);
        }
    }

    IEnumerator FlashRed()
    {
        GetComponentInChildren<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
    }
}

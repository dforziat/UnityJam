using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HomingMissile : EnemyParent
{
    private NavMeshAgent navMeshAgent;
    private AudioSource childAudioSource;
    public Transform spriteHolder;
    public Transform particleHolder;

    // Start is called before the first frame update
    void Start()
    {
        health = 1;
        damage = 10;
        childAudioSource = GetComponent<AudioSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.position);
        if(Vector3.Distance(target.position, transform.position) <= navMeshAgent.stoppingDistance)
        {
            explode(true);
        }
    }

    public new void takeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            explode(false);
            dropItem();
        }
    }

    public void explode(bool damagePlayer)
    {
        if (damagePlayer)
        {
            target.GetComponent<PlayerControls>().TakeDamage(damage);
        }
        Destroy(gameObject);
        Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

    }
}

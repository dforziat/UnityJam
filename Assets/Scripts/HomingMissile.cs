using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HomingMissile : EnemyParent
{
    private NavMeshAgent navMeshAgent;
    private AudioSource childAudioSource;
    public Transform spriteHolder;
    // Start is called before the first frame update
    void Start()
    {
        health = 1;
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
            explode();
        }
    }

    public void explode()
    {

    }
}

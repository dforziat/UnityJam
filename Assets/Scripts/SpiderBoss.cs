using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiderBoss : BossScript
{

    private float walkSpeed = 1f;
    private int maxHealth = 100;
    private bool isDead = false;
 

    public Transform head;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        health = 99;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lookAtPlayer();
        navMeshAgent.SetDestination(playerTransform.position);
    }

    public new void takeDamage(int damage)
    {

        if (health <= 0 && !isDead)
        {
            isDead = true;
            animator.SetTrigger("death");
        }
        health -= damage;

        //playDamagedClip();
    }

    private void lookAtPlayer()
    {
        head.LookAt(playerTransform.position);
    }
}

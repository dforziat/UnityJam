using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    public float distanceToTarget;
    private NavMeshAgent navMeshAgent;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (distanceToTarget <= 2)//2.5f
        {
            Vector3 runTo = transform.position + ((transform.position - target.position) * 2);
            navMeshAgent.SetDestination(runTo);
           // animator.SetBool("backward", true);
            /*  if (navMeshAgent.FindClosestEdge(out NavMeshHit hit))
              {
                  Debug.DrawRay(hit.position, hit.normal, Color.blue, 1.0f);
                  Debug.Log("Backwards destination: " + hit.position);
                  Debug.Log("Backwards Distance: " + hit.distance);
                  navMeshAgent.SetDestination(hit.position);
                  animator.SetBool("backward", true);
              }*/

        }
    }
}

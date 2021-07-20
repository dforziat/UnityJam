using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : MonoBehaviour
{
    public int health = 3;
    private Transform target;
    private bool isProvoked = false;
    private float distanceToTarget = Mathf.Infinity;
    private float detectRange = 5;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

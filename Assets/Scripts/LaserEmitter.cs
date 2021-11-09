using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer lineRenderer;
    public GameObject leftEmitter;
    public GameObject rightEmitter;

    private int damage = 10;
    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, leftEmitter.transform.position);
        lineRenderer.SetPosition(1, rightEmitter.transform.position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.BroadcastMessage("TakeDamage", 10);
        }
    }
}

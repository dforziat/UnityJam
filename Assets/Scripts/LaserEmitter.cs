using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    // Start is called before the first frame update

    private LineRenderer lineRenderer;
    public GameObject leftEmitter;
    public GameObject rightEmitter;
    private BoxCollider boxCollider;
    private ParticleSystem laserParticleSystem;
    private AudioSource audioSource;
    private Animator animator;

    private int damage = 10;
    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
        lineRenderer.enabled = false;

        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        laserParticleSystem = GetComponentInChildren<ParticleSystem>();
        laserParticleSystem.Stop();

        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

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

    public void activateLaser()
    {
        lineRenderer.enabled = true;
        boxCollider.enabled = true;
        laserParticleSystem.Play();
        audioSource.Play();
        animator.SetTrigger("activate");
    }

    public void deactivateLaser()
    {
        lineRenderer.enabled = false;
        boxCollider.enabled = false;
        laserParticleSystem.Stop();
        audioSource.Stop();
        animator.speed = 0;
    }
}

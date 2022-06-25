using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss3Script : MonoBehaviour
{
    // Start is called before the first frame update

    public int health;

    private MeshRenderer[] meshRendererList;
    private List<Color> originalColorList;
    private float flashTime = .2f;
    private bool isDead = false;

    public Animator animator;
    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    public AudioSource walkAudioSource;
    private Transform playerTransform;
    private BoxCollider boxCollider;

    public int saberDamage = 15;


    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = true;
        navMeshAgent.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {

        if (health <= 0 && !isDead)
        {
            isDead = true;
            navMeshAgent.speed = 0;
            animator.SetTrigger("death");
        }
        health -= damage;
        Debug.Log("Boss Health: " + health);

       // playDamagedClip();
    }
}

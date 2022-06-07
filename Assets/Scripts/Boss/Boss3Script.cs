using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss3Script : BossScript
{
    // Start is called before the first frame update

    private MeshRenderer[] meshRendererList;
    private List<Color> originalColorList;
    private float flashTime = .2f;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private AudioSource audioSource;
    public AudioSource walkAudioSource;
    private Transform playerTransform;
    private BoxCollider boxCollider;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        secondaryAudioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        navMeshAgent.enabled = false;

        meshRendererList = GetComponentsInChildren<MeshRenderer>();
        originalColorList = new List<Color>();
        foreach (MeshRenderer mesh in meshRendererList)
        {
            originalColorList.Add(mesh.material.color);
        }

        //Rifle stats
        Vector3 rifleRunPosition = new Vector3(1.2f, 0.56f, 0.67f);
        Vector3 rifleRunRotation = new Vector3(285, 90, 0);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

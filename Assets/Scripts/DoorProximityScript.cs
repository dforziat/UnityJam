using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProximityScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float distToPlayer = Mathf.Infinity;
    private float activationDistance = 2.5f;
    public bool isUnlocked = true;
    private Transform playerTransform;
    private Animator animator;

    public AudioClip doorOpenClip;
    private AudioSource audioSource;
    void Start()
    {
        playerTransform = FindObjectOfType<PlayerControls>().transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if(distToPlayer <= activationDistance && isUnlocked)
        {
           
            animator.SetBool("open", true);          
        }
        else
        {
            animator.SetBool("open", false);
        }

    }

    //Called by Animation Event
    public void playDoorOpeningSFXEvent()
    {
        audioSource.pitch = 1f;
        audioSource.PlayOneShot(doorOpenClip);
    }

    //Called by Animation Event
    public void playDoorClosingSFXEvent()
    {
        audioSource.pitch = -1f;
        audioSource.PlayOneShot(doorOpenClip);
    }
}

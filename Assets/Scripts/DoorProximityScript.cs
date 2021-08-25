using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorProximityScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float distToPlayer = Mathf.Infinity;
    private float activationDistance = 2.5f;
    public bool isUnlocked = true;
    private bool isNearEnemy = false;
    private Transform playerTransform;
    private Animator animator;
    private float doorCloseTime = 1f;
    private bool autoDoorShut = false;
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
        if (isUnlocked)
        {
            if (distToPlayer <= activationDistance || isNearEnemy)
            {
                animator.SetBool("open", true);
            }
            else 
            {
                animator.SetBool("open", false);
                doorCloseTime = 1f;
            }
        }

        if (autoDoorShut)
        {
            doorCloseTime -= Time.deltaTime;
            if(doorCloseTime <= 0)
            {
                isNearEnemy = false;
            }
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag + ": entered door collider");
        if (other.gameObject.CompareTag("Enemy"))
        {
            isNearEnemy = true;
            autoDoorShut = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            isNearEnemy = false;
        }
    } 

}

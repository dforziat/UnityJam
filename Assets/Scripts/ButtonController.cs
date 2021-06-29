using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isActivated = false;
    public GameObject door;
    private Animator animator;
    public AudioClip buttonClip;
    private AudioSource audioSource;
    void Start()
    {
        animator = GetComponent<Animator>();
        door.GetComponent<DoorProximityScript>().isUnlocked = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        isActivated = true;
        animator.SetTrigger("activate");
        door.GetComponent<DoorProximityScript>().isUnlocked = true;
        audioSource.PlayOneShot(buttonClip);
    }
}

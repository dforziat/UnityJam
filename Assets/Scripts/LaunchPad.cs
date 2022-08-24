using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float jumpHeight = 100f;
    private bool hasCollided = false;

    public AudioClip humClip;
    public AudioClip launchClip;
    private AudioSource audioSource; 
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
      //  Debug.Log("Collided with player");
        if (other.tag == "Player" && !hasCollided)
        {
            audioSource.PlayOneShot(launchClip);
            other.GetComponent<PlayerControls>().Jump(jumpHeight);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hasCollided = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemScript : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public AudioClip buttonChangeClip;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    public void playButtonChangeClip()
    {
        audioSource.PlayOneShot(buttonChangeClip);
    }
}

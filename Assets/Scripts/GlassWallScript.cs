using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassWallScript : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    private Animator animator;
    public AudioClip clickClip;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playClickClip()
    {
        audioSource.PlayOneShot(clickClip);
    }

    public void activateAnimation()
    {
        animator.SetTrigger("activate");
    }

    public void die()
    {
        Destroy(gameObject);
    }
}

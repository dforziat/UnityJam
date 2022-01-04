using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip healthFillClip;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playHealthFillClip()//Called By Animation Event
    {
        audioSource.PlayOneShot(healthFillClip);
    }
}

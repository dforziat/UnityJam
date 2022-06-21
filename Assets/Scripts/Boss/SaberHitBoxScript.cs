using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberHitBoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Boss3Script parentScript;
    public AudioSource audioSource;

    public AudioClip saberHitClip;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerControls>().TakeDamage(parentScript.saberDamage);
            audioSource.PlayOneShot(saberHitClip);
        }
    }
}

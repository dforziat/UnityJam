using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmoPickup : MonoBehaviour
{
    // Start is called before the first frame update
    int ammoAmount = 20;
    private AudioSource audioSource;
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
        if(other.tag == "Player")
        {
            audioSource.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().RecieveShotgunAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}

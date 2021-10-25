using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public AudioClip beepClip;
    public GameObject explosion;

    private int explosionDamage = 10;
    private float explosionDistance = 1f;
    private float verticalOffset = .1f;
    private Transform player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void beep()
    {
        GetComponent<AudioSource>().PlayOneShot(beepClip);
    }

    public void explode()
    {
        if (Vector3.Distance(player.position, transform.position) <= explosionDistance)
        {
            player.gameObject.BroadcastMessage("TakeDamage", explosionDamage);
        }
        var bomb = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + verticalOffset, transform.position.z), transform.rotation);
        bomb.transform.localScale += new Vector3(2, 2, 2);
        Destroy(gameObject);
    }
}

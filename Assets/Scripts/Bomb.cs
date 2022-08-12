using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public AudioClip beepClip;
    public GameObject explosion;
    public GameObject ammoPickup;
    public GameObject healthPickup;

    private int explosionDamage = 10;
    private float explosionDistance = 3f;
    private float verticalOffset = .1f;
    private Transform player;

    public bool isGrenade = false;


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
        if (GetComponent<AudioSource>().enabled)
        {
            GetComponent<AudioSource>().PlayOneShot(beepClip);
        }
    }
        
       
    

    public void explode()
    {
        if (Vector3.Distance(player.position, transform.position) <= explosionDistance)
        {
            player.gameObject.BroadcastMessage("TakeDamage", explosionDamage);
        }

        //ammo drop
        int dropRandomNum = Random.Range(0, 100);
        if (dropRandomNum <= 10)
        {
            int randomDrop = Random.Range(0, 10);
            if(randomDrop <= 5)
            {
                Instantiate(ammoPickup, new Vector3(transform.position.x, transform.position.y + verticalOffset, transform.position.z), transform.rotation);
            }
            else
            {
                Instantiate(healthPickup, new Vector3(transform.position.x, transform.position.y + verticalOffset, transform.position.z), transform.rotation);
            }
        }

        if (isGrenade)
        {
            GameObject launchBomb = Instantiate(gameObject);
            GameObject launchBombL = Instantiate(gameObject);
            GameObject launchBombR = Instantiate(gameObject);

            launchBomb.transform.localScale -= new Vector3(.5f, .5f, .5f);
            launchBombL.transform.localScale -= new Vector3(.5f, .5f, .5f);
            launchBombR.transform.localScale -= new Vector3(.5f, .5f, .5f);

            launchBomb.GetComponent<Bomb>().isGrenade = false;
            launchBombL.GetComponent<Bomb>().isGrenade = false;
            launchBombR.GetComponent<Bomb>().isGrenade = false;


            //diasble audio on 2/3 bombs
            launchBombL.GetComponent<AudioSource>().enabled = false;
            launchBombR.GetComponent<AudioSource>().enabled = false;

            launchBomb.transform.position = transform.position;
            launchBombL.transform.position = transform.position;
            launchBombR.transform.position = transform.position;

            launchBomb.GetComponent<Rigidbody>().AddExplosionForce(15, launchBomb.transform.position, 10);

        }

        var bomb = Instantiate(explosion, new Vector3(transform.position.x, transform.position.y + verticalOffset, transform.position.z), transform.rotation);
        bomb.transform.localScale += new Vector3(2, 2, 2);
        Destroy(gameObject);
    }
}

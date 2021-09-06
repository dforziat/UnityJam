using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public int health = 3;
    public int damage = 10;
    public bool isProvoked = false;
    private float verticalOffset = 0;
    public GameObject explosion;
    public int droprate = 20; //20% drop rate of pickup
    public GameObject ammoPickup;
    public GameObject healthPickup;

    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public AudioClip damagedClip;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        isProvoked = true;
        health -= damage;
        StartCoroutine(FlashRed());

        if (health <= 0)
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y - verticalOffset, transform.position.z), transform.rotation);
            dropItem();
            Destroy(gameObject);
        }
        if (audioSource.enabled == true)
        {
            audioSource.PlayOneShot(damagedClip);
        }
    }

    IEnumerator FlashRed()
    {
        Debug.Log("Flash Red");
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(.1f);
        spriteRenderer.color = Color.white;
    }

    public void dropItem()
    {
        int dropRandomNum = Random.Range(0, 100);
        if(dropRandomNum <= droprate)
        {
            int ammoOrHealthRandomNum = Random.Range(1, 2);
            switch (ammoOrHealthRandomNum)
            {
                case 1:
                    //create ammo pickup
                    Instantiate(ammoPickup, new Vector3(transform.position.x, transform.position.y - verticalOffset, transform.position.z), transform.rotation);
                    break;
                case 2:
                    Instantiate(healthPickup, new Vector3(transform.position.x, transform.position.y - verticalOffset, transform.position.z), transform.rotation);
                    break;
            }

        }

    }
}

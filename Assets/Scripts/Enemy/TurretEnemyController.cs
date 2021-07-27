using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemyController : MonoBehaviour
{
    public int health = 3;
    private Transform target;
    private bool isDetected = false;
    private float distanceToTarget = Mathf.Infinity;
    private float detectRange = 5f;
    private Animator animator;
    private float bulletSpeed = 5;
    private float yOffset = .2f;

    public GameObject bullet;
    public GameObject explosion;
    public AudioClip alertClip;
    public AudioClip damagedClip;
    public AudioClip shootClip;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= detectRange && !isDetected)
        {
            Debug.Log("Is Detected");
            audioSource.PlayOneShot(alertClip);
            GetComponent<CapsuleCollider>().enabled = true;
            animator.SetBool("detected", true);
            isDetected = true;
        }
        if(distanceToTarget > detectRange && isDetected)
        {
            Debug.Log("Lost Detection");
            GetComponent<CapsuleCollider>().enabled = false;
            animator.SetBool("detected", false);
            isDetected = false;
        }
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        audioSource.PlayOneShot(damagedClip);
        if (health <= 0)
        {
            Instantiate(explosion, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
    }



    public void fireBulletEvent()//This method is called by an Animation event
    {
        audioSource.PlayOneShot(shootClip);
        GameObject shootingBullet = Instantiate(bullet);
        shootingBullet.transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        shootingBullet.GetComponent<Rigidbody>().velocity = transform.forward.normalized * bulletSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCannon : MonoBehaviour
{
    // Start is called before the first frame update

    public float fireRate = 120f;
    public float timer;
    public float projectileSpeed = 3f;
    public GameObject projectile;

    public AudioClip cannonShotClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = fireRate;   
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            shootCannon();
            timer = fireRate;
        }

    }

    public void shootCannon()
    {
        audioSource.PlayOneShot(cannonShotClip);
        GameObject cannonShot = Instantiate(projectile, transform.position, transform.rotation);
        cannonShot.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 2;
        Gizmos.DrawRay(transform.position, direction);
    }


}

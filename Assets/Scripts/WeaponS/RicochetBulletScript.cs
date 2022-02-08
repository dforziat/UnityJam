using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBulletScript : MonoBehaviour
{
    float speed = 10;
    public LayerMask wallMask;
    public LayerMask groundMask;
    public LayerMask clutterMask;
    public LayerMask enemyMask;
    public LayerMask bossMask;
    public LayerMask defaultMask;
    public AudioClip impactClip;
    private AudioSource audioSource;

    int bounces = 0;
    int bounceLimit = 4;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        bounceDetect();
        limitBounces();
    }

    void bounceDetect()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f, defaultMask))
        {
            if (hit.transform.tag == "Button")
            {
                Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
                float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, rot, 0);
                bounces++;
                hit.transform.GetComponent<ButtonController>().Activate();
            }
        }

        if (Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f, wallMask) ||
            Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f, groundMask) ||
            Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f, clutterMask) )
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            bounces++;
            Debug.Log(bounces + "Wall hit");
            audioSource.PlayOneShot(impactClip);
        }

        if (Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f, enemyMask) ||
            Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f, bossMask))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            bounces++;
            hit.transform.BroadcastMessage("takeDamage", 2);
            Debug.Log(bounces + "Enemy hit");



        }


    }

    void limitBounces()
    {
        if (bounces > bounceLimit)
        {
            Destroy(gameObject);
        }
    }

}

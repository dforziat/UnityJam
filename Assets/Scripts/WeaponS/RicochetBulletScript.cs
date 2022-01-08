using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RicochetBulletScript : MonoBehaviour
{
    float speed = 15;
    public LayerMask collisionMask1;
    public LayerMask collisionMask2;
    int bounces = 0;
    int bounceLimit = 3;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);


        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * speed +.1f, collisionMask1))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            bounces++;
            Debug.Log(bounces + "Wall hit");
        }

        if (Physics.Raycast(ray, out hit, Time.deltaTime * speed + .1f, collisionMask2))
        {
            Vector3 reflectDir = Vector3.Reflect(ray.direction, hit.normal);
            float rot = 90 - Mathf.Atan2(reflectDir.z, reflectDir.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, rot, 0);
            bounces++;
            Debug.Log(bounces +"Enemy hit");

        }

        if (bounces > bounceLimit)
        {
            Destroy(gameObject);
        }

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    // Start is called before the first frame update
    int damage = 5;

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
            other.BroadcastMessage("TakeDamage", damage);
        }else if(other.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }

}

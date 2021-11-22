using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDashHurtbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.transform.BroadcastMessage("takeDamage", 5);
        }

        if (other.tag == "BreakableWall")
        {
            Debug.Log("BREAK");
            Destroy(other);
        }
    }
}

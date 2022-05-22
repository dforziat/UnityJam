using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearDashHurtbox : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject brokenWall;
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
            other.transform.SendMessageUpwards("takeDamage", 5);
        }

        if (other.tag == "Spear")
        {
            Destroy(other.gameObject);
            Instantiate(brokenWall, other.transform.position, other.transform.rotation);

            Debug.Log("Destroy Wall");
        }
        if(other.tag == "BrokenWall")
        {
            other.transform.DetachChildren();
            Debug.Log("Applying FORCE!");
            other.GetComponentInChildren<Rigidbody>().AddExplosionForce(200, transform.position, 2);
        }
    }
}

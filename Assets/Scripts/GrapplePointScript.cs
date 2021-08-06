using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePointScript : MonoBehaviour
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
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControls>().isGrappling = false;
            //GameObject.FindGameObjectWithTag("WeaponsHud").GetComponentInChildren<GrapplegunScript>().ShootingRecovery();
            if(GameObject.FindGameObjectWithTag("WeaponsHud").GetComponentInChildren<GrapplegunScript>() != null)
            {
                GameObject.FindGameObjectWithTag("WeaponsHud").GetComponentInChildren<GrapplegunScript>().ShootingRecovery();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private int healAmmount = 20;
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
           PlayerControls playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
            if (playerControls.curHp < playerControls.maxHp)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>().Heal(healAmmount);
                Destroy(gameObject);
            }
        }
    }
}

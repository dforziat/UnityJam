using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health = 50;

    private Animator animator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }


    public void takeDamage(int damage)
    {
        health -= damage;
        //play damage audio sfx. Maybe have 2 or 3 for diversity
        if(health <= 0)
        {
            //start death sequence, maybe even a cutscene. 
        }
    }
}

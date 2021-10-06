using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health = 50;
    private int chargeShotDamage = 20;
    public GameObject chargeShotObject;
    private float chargeShotSpeed = 8f;
    public Transform leftCannon;
    public Transform rightCannon;

    private Transform target;
    private Animator animator;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
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

    public void shootChargeShot()//this is triggered by an animation
    {
       // audioSource.PlayOneShot(shootClip);
        GameObject chargeShotL = Instantiate(chargeShotObject);
        GameObject chargeShotR = Instantiate(chargeShotObject);
        chargeShotL.transform.position = new Vector3(leftCannon.position.x, leftCannon.position.y, leftCannon.position.z + 1);
        chargeShotR.transform.position = new Vector3(rightCannon.position.x, rightCannon.position.y, rightCannon.position.z + 1);
        chargeShotL.transform.LookAt(new Vector3(target.position.x, target.position.y - 1f, target.position.z));
        chargeShotR.transform.LookAt(new Vector3(target.position.x, target.position.y - 1f, target.position.z));
        chargeShotL.GetComponent<Rigidbody>().velocity = transform.forward.normalized * chargeShotSpeed;
        chargeShotR.GetComponent<Rigidbody>().velocity = transform.forward.normalized * chargeShotSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health = 50;
    private int chargeShotDamage = 20;
    public GameObject chargeShotObject;
    private float chargeShotSpeed = 10f;
    public Transform leftCannon;
    public Transform rightCannon;

    private Transform target;
    private Animator animator;

    //audio
    public AudioSource secondaryAudioSource;
    public AudioClip damagedClip;
    public AudioClip chargeClip;
    public AudioClip chargeShotClip;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }


    public void takeDamage(int damage)
    {
        health -= damage;
        secondaryAudioSource.PlayOneShot(damagedClip);
        //play damage audio sfx. Maybe have 2 or 3 for diversity
        if (health <= 0)
        {
            //start death sequence, maybe even a cutscene. 
        }
    }

    public void shootChargeShot()//this is triggered by an animation
    {
        GameObject chargeShotL = Instantiate(chargeShotObject);
        GameObject chargeShotR = Instantiate(chargeShotObject);
        chargeShotL.transform.position = new Vector3(leftCannon.position.x, leftCannon.position.y, leftCannon.position.z + 1);
        chargeShotR.transform.position = new Vector3(rightCannon.position.x, rightCannon.position.y, rightCannon.position.z + 1);

        var Ldir = (target.position - chargeShotL.transform.position).normalized;
        var Rdir = (target.position - chargeShotR.transform.position).normalized;

        chargeShotR.GetComponent<Rigidbody>().velocity = Rdir * chargeShotSpeed;
        chargeShotL.GetComponent<Rigidbody>().velocity = Ldir * chargeShotSpeed;

        secondaryAudioSource.PlayOneShot(chargeShotClip);
    }

    public void chargeShotState()
    {
        //start animation
        //beging playing chargeshot sfx
        //call from idle state
    }
}

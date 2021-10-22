using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int health = 50;
    private int chargeShotDamage = 20;
    public GameObject chargeShotObject;
    private float chargeShotSpeed = 10f;
    private float machinegunBulletSpeed = 20f;
    private int attackState = 0;
    public Transform leftCannon;
    public Transform rightCannon;
    public ParticleSystem LeftCannonParticle;
    public ParticleSystem RightCannonParticle;

    public LineRenderer lineRenderer;
    public Transform machineGunBarrel;

    private Transform target;
    private Animator animator;

    //audio
    public AudioSource secondaryAudioSource;
    public AudioClip damagedClip;
    public AudioClip chargeClip;
    public AudioClip chargeShotClip;
    public AudioClip machinegunClip;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        LeftCannonParticle.Stop();
        RightCannonParticle.Stop();
        lineRenderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

        //maching gun barrel end
        lineRenderer.SetPosition(0, machineGunBarrel.position);
        lineRenderer.SetPosition(1, new Vector3(target.position.x, target.position.y -.5f, target.position.z));
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
        animator.SetTrigger("chargeShot");
        secondaryAudioSource.PlayOneShot(chargeClip);

    }

    public void chooseNextAttack()//called by idle animation end
    {
        switch (attackState)
        {
            case 1:
                chargeShotState();
                break;
            case 2:
                machinegunstate();
                break;
            case 3:
                //mineshotState();
            default:
                break;
        }

        //increment attack state
        if (attackState < 3)
        {
            attackState++;
        }
        else {
            attackState = 1;
        }
    }

    public void resetToIdleState()
    {
        Debug.Log("Set to Idle State");
        secondaryAudioSource.Stop();

        LeftCannonParticle.Stop();
        RightCannonParticle.Stop();
    }

    public void startChargeParticles()
    {
        LeftCannonParticle.Play();
        RightCannonParticle.Play();
    }

    public void machinegunstate()
    {
        animator.SetTrigger("machinegun");
    }

    public void shootMachinegun()//Call through animation event
    {
        GameObject machinegunBullet = Instantiate(chargeShotObject);
        machinegunBullet.transform.position = machineGunBarrel.position;

        var dir = (target.position - machinegunBullet.transform.position).normalized;

        machinegunBullet.GetComponent<Rigidbody>().velocity = dir * machinegunBulletSpeed;

        secondaryAudioSource.PlayOneShot(machinegunClip);
    }
}

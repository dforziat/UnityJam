using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class BossScript : MonoBehaviour
{
    [Header("Stats")]
    private int maxHealth = 100;
    public int health;
    private Transform target;
    private Animator animator;
    private int attackState = 1;
    private bool isSecondStage = false;
    public GameObject jumppads;
    public GameObject explosion;
    public DoorProximityScript bossDoor;
    public LaserEmitter laser;
    private bool isDead = false;
    private MeshRenderer[] meshRendererList;
    private List<Color> originalColorList;
    private float flashTime = .2f;

    [Header("Charge Shot")]
    private float chargeShotSpeed = 10f;
    private int chargeShotDamage = 20;
    public GameObject chargeShotObject;
    public Transform leftCannon;

    public Transform rightCannon;
    public ParticleSystem LeftCannonParticle;
    public ParticleSystem RightCannonParticle;

    [Header("Machinegun")]
    private float machinegunBulletSpeed = 20f;
    public LineRenderer lineRenderer;
    public Transform machineGunBarrel;

    [Header("Bomb Launcher")]
    public float bombLaunchPower = 10f;
    public GameObject bomb;
    public Transform bombLaunchLocation;

    [Header("Audio")]
    public AudioSource secondaryAudioSource;
    public AudioClip damagedClip;
    public AudioClip damagedClip2;
    public AudioClip damagedClip3;
    public AudioClip chargeClip;
    public AudioClip chargeShotClip;
    public AudioClip machinegunClip;
    public AudioClip machinegunTrackingClip;
    public AudioClip bombLaunchClip;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        LeftCannonParticle.Stop();
        RightCannonParticle.Stop();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        meshRendererList = GetComponentsInChildren<MeshRenderer>();
        originalColorList = new List<Color>();
        foreach (MeshRenderer mesh in meshRendererList)
        {
            originalColorList.Add(mesh.material.color);
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

        //maching gun barrel end
        lineRenderer.SetPosition(0, machineGunBarrel.position);
        lineRenderer.SetPosition(1, new Vector3(target.position.x, target.position.y -.5f, target.position.z));

        if(attackState != 2)
        {
            lineRenderer.enabled = false;
        }

    }


    public void takeDamage(int damage)
    {
        if (health <= (maxHealth / 2) && !isSecondStage)
        {
            activateSecondStage();
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            animator.SetTrigger("death");
            if (SteamManager.Initialized)
            {
                SteamUserStats.SetAchievement(SteamAchievementConstants.ACT_1);
            }
        }
        health -= damage;

        foreach (MeshRenderer mesh in meshRendererList)
        {
            mesh.material.color = Color.red;

        }
        Invoke("ResetColor", flashTime);

        playDamagedClip();
    }

    private void ResetColor()
    {
        int colorListPlace = 0;
        foreach (MeshRenderer mesh in meshRendererList)
        {
            mesh.material.color = originalColorList[colorListPlace];
            colorListPlace++;
        }
        colorListPlace = 0;
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
                bombstate();
                break;
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
       // secondaryAudioSource.Stop();

        LeftCannonParticle.Stop();
        RightCannonParticle.Stop();

        lineRenderer.enabled = false;
    }

    public void startChargeParticles()
    {
        LeftCannonParticle.Play();
        RightCannonParticle.Play();
    }

    public void machinegunstate()
    {
        animator.SetTrigger("machinegun");
        secondaryAudioSource.PlayOneShot(machinegunTrackingClip);
    }

    public void bombstate()
    {
        animator.SetTrigger("bomb");
    }

    public void shootMachinegun()//Call through animation event
    {
        GameObject machinegunBullet = Instantiate(chargeShotObject);
        machinegunBullet.transform.position = machineGunBarrel.position;

        var dir = (target.position - machinegunBullet.transform.position).normalized;

        machinegunBullet.GetComponent<Rigidbody>().velocity = dir * machinegunBulletSpeed;

        secondaryAudioSource.PlayOneShot(machinegunClip);
    }

    public void throwBomb()
    {
        GameObject launchBomb = Instantiate(bomb);
        GameObject launchBombL = Instantiate(bomb);
        GameObject launchBombR = Instantiate(bomb);

        //diasble audio on 2/3 bombs
        launchBombL.GetComponent<AudioSource>().enabled = false;
        launchBombR.GetComponent<AudioSource>().enabled = false;

        launchBomb.transform.position = bombLaunchLocation.position;
        launchBombL.transform.position = bombLaunchLocation.position;
        launchBombR.transform.position = bombLaunchLocation.position;

        

        var dir = (target.position - launchBomb.transform.position).normalized;
        var dirL = Quaternion.AngleAxis(30, Vector3.forward) * dir;
        var dirR = Quaternion.AngleAxis(-30, Vector3.forward) * dir; ;

        float randPow = Random.Range(-2f, 2f);

        launchBomb.GetComponent<Rigidbody>().velocity = dir * (bombLaunchPower + randPow);
        launchBombL.GetComponent<Rigidbody>().velocity = dirL * (bombLaunchPower + randPow);
        launchBombR.GetComponent<Rigidbody>().velocity = dirR * (bombLaunchPower + randPow);

        secondaryAudioSource.PlayOneShot(bombLaunchClip);
    }

    public void activateSecondStage()
    {
        animator.SetTrigger("secondstage");
        isSecondStage = true;
        jumppads.SetActive(true);
        laser.activateLaser();
    }

    public void explode()
    {
        float max = 1.5f;
        float min = -1.5f;
        float randomX = Random.Range(max, min);
        float randomY = Random.Range(max, min);
        float randomZ = Random.Range(max, min);
        Instantiate(explosion, new Vector3(transform.position.x + randomX, transform.position.y + randomY + 1f, transform.position.z + randomZ), transform.rotation);
    }

    public void die()
    {
        bossDoor.isUnlocked = true;
        laser.deactivateLaser();
        Destroy(gameObject);
    }

    private void playDamagedClip()
    {
        int randNum = Random.Range(1, 4);
        switch (randNum)
        {
            case 1:
                secondaryAudioSource.PlayOneShot(damagedClip);
                break;
            case 2:
                secondaryAudioSource.PlayOneShot(damagedClip2);
                break;
            case 3:
                secondaryAudioSource.PlayOneShot(damagedClip3);
                break;
        }
    }
}

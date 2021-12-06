using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : MonoBehaviour
{

    public GameObject electricParticle;
    public GameObject secondaryProbe;
    public GameObject hitBox;

    private AudioSource audioSource;
    private bool isOn = false;
    public float intervalTime;
    private float timeRemaining;
    private int damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
        timeRemaining = intervalTime;
        audioSource = GetComponent<AudioSource>();
        turnOffElectricity();
    }

    // Update is called once per frame
    void Update()
    {
        autoAimElectricity();
        //timer
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            if (isOn)
            {
                turnOffElectricity();
            }
            else
            {
                turnOnElectricity();
            }
            timeRemaining = intervalTime;
        }


    }

    public void turnOnElectricity()
    {
        audioSource.Play();
        electricParticle.SetActive(true);
        hitBox.SetActive(true);
        isOn = true;
    }

    public void turnOffElectricity()
    {
        audioSource.Stop();
        electricParticle.SetActive(false);
        hitBox.SetActive(false);
        isOn = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.BroadcastMessage("TakeDamage", damage);
        }
    }

    public void autoAimElectricity()
    {
        // Determine which direction to rotate towards
        Vector3 targetDirection = secondaryProbe.transform.position - electricParticle.transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = 1 * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(electricParticle.transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        electricParticle.transform.rotation = Quaternion.LookRotation(newDirection);
    }

}

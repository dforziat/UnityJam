using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretSpawnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject turret;
    public Transform spawnLocation;
    private GameObject spawnedTurret;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        spawnTurret();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedTurret == null)
        {
            animator.SetTrigger("spawn");
        }
    }

    public void spawnTurret()
    {
        if(spawnedTurret == null)
        {
            spawnedTurret = Instantiate(turret, spawnLocation);
            spawnedTurret.GetComponent<TurretEnemyController>().detectRange = 4;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        RougeManager rogueManager = FindObjectOfType<RougeManager>();
        rogueManager.spawnWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .25f);
    }
}

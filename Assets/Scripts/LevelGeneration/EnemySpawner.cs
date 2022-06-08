using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyList;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnEnemy()
    {
        int randomNum = Random.Range(0, enemyList.Length);
        Instantiate(enemyList[randomNum], transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, .25f);
    }
}

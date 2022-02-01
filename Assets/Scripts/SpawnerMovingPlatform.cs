using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMovingPlatform : MonoBehaviour
{

    public GameObject widePlatform;

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("platformSpawner", 0f, 5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void platformSpawner()
    {
        Instantiate(widePlatform, new Vector3(21.48717f, 8.359883f, 47.51f), Quaternion.identity);
    }
}

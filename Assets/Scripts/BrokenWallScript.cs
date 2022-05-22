using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float timer = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}

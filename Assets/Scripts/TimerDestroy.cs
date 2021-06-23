using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private float destroyTimer = .6f;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyTimer);
    }
}

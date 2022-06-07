using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 2;
        Gizmos.DrawRay(transform.position, direction);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperRoomTrigger : MonoBehaviour
{

    public DoorProximityScript sniperRoomDoor;
    public Boss3Script boss3Script;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            sniperRoomDoor.isUnlocked = false;
            boss3Script.state = "snipe";
            Destroy(gameObject);
        }
    }

    public void OnDrawGizmos()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}

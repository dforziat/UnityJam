using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRoomDoorLock : MonoBehaviour
{
    // Start is called before the first frame update

    public DoorProximityScript secondRoomDoor;
    public Boss3Script boss3Script;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            secondRoomDoor.isUnlocked = false;
            boss3Script.state = "ANIMATION";
            boss3Script.chooseRandomAttack();
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow cube at the transform position
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}

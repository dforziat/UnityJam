using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;

public class LevelMakerScript : MonoBehaviour
{
    // Start is called before the first frame update
    int numOfRoomsMin = 3;
    int numOfRoomsMax = 16;
    int currentNumOfRooms = 0;
    GameObject initialExitPoint;
    public GameObject initialRoomBoundBox;

    //Test with 3RoomPrefab;
    public GameObject testRoom;
    public GameObject wall;

    NavMeshBuilder navMeshBuilder;

    void Start()
    {
        navMeshBuilder = new NavMeshBuilder();
        Debug.Log("Level Maker started");
        initialExitPoint = GameObject.FindGameObjectWithTag("ExitPoint");
        GenerateLevel(initialExitPoint);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateLevel(GameObject exitPoint)
    {
      //  Debug.Log("Exit Point Location: " + exitPoint.transform.position);
        //Instantiate(testRoom, exitPoint.transform.position, exitPoint.transform.rotation);
        checkForRoomCollision(exitPoint);
        //Debug.Log("Room Num: " + currentNumOfRooms + " Created!");
        if(currentNumOfRooms < numOfRoomsMax)
        {
            GenerateLevel(GameObject.FindWithTag("ExitPoint"));
        }
        else
        {
            runFinalPass();
        }
    }

    public void checkForRoomCollision(GameObject exitPoint)
    {
        //Get All other bounding boxes in the scene and check to see if they intersect.
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        //check bounding box to see if it intersects with another object, if it does then return false and restart
        GameObject testCollRoom = Instantiate(testRoom, exitPoint.transform.position, exitPoint.transform.rotation);
        testCollRoom.name = "RoomNum " + currentNumOfRooms;
        foreach (GameObject room in rooms)
        {
            if (testCollRoom.GetComponent<BoxCollider>().bounds.Intersects(room.GetComponent<BoxCollider>().bounds) || testCollRoom.GetComponent<BoxCollider>().bounds.Intersects(initialRoomBoundBox.GetComponent<BoxCollider>().bounds))//check for the starting room boxCollider too
            {
                Debug.Log("Room Collision! " + testCollRoom.name);
                Destroy(testCollRoom);
                Instantiate(wall, exitPoint.transform.position, exitPoint.transform.rotation);
            }
        }
        currentNumOfRooms++;
        exitPoint.SetActive(false);
    }

    public void runFinalPass()
    {
        GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("ExitPoint");
        foreach(GameObject exitPoint in exitPoints)
        {
            Instantiate(wall, exitPoint.transform.position, exitPoint.transform.rotation);
            exitPoint.SetActive(false);
        }

        NavMeshBuilder.BuildNavMesh();
    }
}

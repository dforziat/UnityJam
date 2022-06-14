using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;

public class LevelMakerScript : MonoBehaviour
{
    public int numOfRoomsMax = 5;
    int currentNumOfRooms = 0;
    bool exitRoomPlaced = false;
    GameObject initialExitPoint;
    public GameObject initialRoomBoundBox;

    //Test with 3RoomPrefab;
    public GameObject testRoom;
    public GameObject wall;
    public GameObject exitRoom;
    public GameObject[] roomList;

    NavMeshBuilder navMeshBuilder;

    void Start()
    {
        navMeshBuilder = new NavMeshBuilder();
        initialExitPoint = GameObject.FindGameObjectWithTag("ExitPoint");
        GenerateLevel(initialExitPoint);
        runFinalPass();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateLevel(GameObject exitPoint)
    {
        if (currentNumOfRooms >= numOfRoomsMax)
        {
            return;
        }
 
        checkForRoomCollision(exitPoint);
        if(currentNumOfRooms < numOfRoomsMax)
        {
            GenerateLevel(getRandomExitPoint());
        }
    }

    public void checkForRoomCollision(GameObject exitPoint)
    {


         GameObject roomType = getRoomType();
        //Get All other bounding boxes in the scene and check to see if they intersect.
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        //check bounding box to see if it intersects with another object, if it does then return false and restart
        GameObject collRoom = Instantiate(roomType, exitPoint.transform.position, exitPoint.transform.rotation);
        collRoom.name = "RoomNum " + currentNumOfRooms;
        foreach (GameObject room in rooms)
        {
            if (collRoom.GetComponent<BoxCollider>().bounds.Intersects(room.GetComponent<BoxCollider>().bounds))
            {
                collRoom.SetActive(false);
                Destroy(collRoom);
                Instantiate(wall, exitPoint.transform.position, exitPoint.transform.rotation);
                exitPoint.SetActive(false);
                GenerateLevel(getRandomExitPoint());
            }
        }
        currentNumOfRooms++;
        exitPoint.SetActive(false);
    }

    public void runFinalPass()
    {

        //Remove Bounding box on all placed rooms.
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Room");
        foreach(GameObject room in rooms)
        {
            room.GetComponent<BoxCollider>().enabled = false;
        }


        GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("ExitPoint");
        //Block All Remaining Gaps will walls
        foreach (GameObject exitPoint in exitPoints)
        {
            Instantiate(wall, exitPoint.transform.position, exitPoint.transform.rotation);
            exitPoint.SetActive(false);
        }

        NavMeshBuilder.BuildNavMesh();

        spawnEnemies();
    }

    public GameObject getRandomExitPoint() 
    {
        GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("ExitPoint");
        int randomNum = Random.Range(0, exitPoints.Length);
        return exitPoints[randomNum];
    }

    public GameObject getRoomType()
    {
        if(currentNumOfRooms + 1 >= numOfRoomsMax)
        {
            return exitRoom;
        }
        else
        {
            //Get Random room from list
            int randomNum = Random.Range(0, roomList.Length);
            return roomList[randomNum];
        }
    }

    public void spawnEnemies()
    {
        EnemySpawner[] spawners = GameObject.FindObjectsOfType<EnemySpawner>();
        foreach(EnemySpawner spawner in spawners)
        {
            Debug.Log("Spawn Enemy!");
            spawner.spawnEnemy();
        }
    }
}

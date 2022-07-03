using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public Transform breakableWallTransform;
    public GameObject breakableWall;
    void Start()
    {
        animator = GetComponent<Animator>();
        breakableWallTransform.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Wall Spawner Trigger");
            Instantiate(breakableWall, breakableWallTransform.position, breakableWallTransform.rotation);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
     spawnWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnWeapon()
    {
        RougeManager rogueManager = FindObjectOfType<RougeManager>();
        int randNum = Random.Range(0, rogueManager.weaponList.Count);
        Debug.Log("Random Num: " + randNum + " Weapon List size: " + rogueManager.weaponList.Capacity);
        GameObject randomWeapon = (GameObject)rogueManager.weaponList[randNum];
        rogueManager.weaponList.RemoveAt(randNum);
        Instantiate(randomWeapon, transform.position, transform.rotation);
    }

    public void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, .25f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RougeManager : MonoBehaviour
{
    // Start is called before the first frame update
    int levelsCompleted = 0;
    int totalTime = 0;
    //ArrayList weaponList = new ArrayList() {PlayerPrefsConstants.SHOTGUN, PlayerPrefsConstants.GRAPPLEGUN, PlayerPrefsConstants.RICHOCHETGUN, PlayerPrefsConstants.MACHINEGUN, PlayerPrefsConstants.SPEAR};
    public List<GameObject> weaponList;
    public Transform weaponSpawner;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnWeapon()
    {
        int randNum = Random.Range(0, weaponList.Capacity);
        GameObject randomWeapon = (GameObject) weaponList[randNum];
        weaponList.RemoveAt(randNum);
        Instantiate(randomWeapon, weaponSpawner.position, weaponSpawner.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RougeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int numLevelsCompleted = 0;
    public float totalTime = 0;
    public List<GameObject> weaponList;

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
        Transform weaponSpawner = GameObject.FindObjectOfType<WeaponSpawner>().gameObject.transform;
        int randNum = Random.Range(0, weaponList.Capacity);
        GameObject randomWeapon = (GameObject) weaponList[randNum];
        weaponList.RemoveAt(randNum);
        Instantiate(randomWeapon, weaponSpawner.position, weaponSpawner.rotation);
    }

  

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

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
    public List<string> unlockedWeaponList;
    public GameObject loadScreen;

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


    public void nextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        loadScreen.SetActive(true);
    }
}

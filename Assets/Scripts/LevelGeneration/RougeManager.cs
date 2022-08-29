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
    public static RougeManager instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject go = new GameObject("RogueManager");
            go.AddComponent<RougeManager>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }



    public void destroySelf()
    {
        Destroy(gameObject);
    }
}

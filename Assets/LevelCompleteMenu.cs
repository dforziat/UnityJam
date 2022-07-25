using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject loadScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(PlayerPrefs.GetInt(PlayerPrefsConstants.CUR_LVL));
        Instantiate(loadScreen, Vector3.zero, new Quaternion(0, 0, 0, 0));
    }
}

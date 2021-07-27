using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{

    public Canvas levelCompleteCanvas;
    // Start is called before the first frame update
    void Start()
    {
        levelCompleteCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        levelCompleteCanvas.enabled = true;
        Time.timeScale = 0;
    }
}

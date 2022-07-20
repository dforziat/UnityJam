using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameOverCanvas;
    void Start()
    {
        gameOverCanvas.SetActive(false);
    }

    public void HandleDeath()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameOverCanvas;
    void Start()
    {
        gameOverCanvas = GameObject.Find("GameOver Canvas");
        gameOverCanvas.SetActive(false);
    }

    public void HandleDeath()
    {
        gameOverCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(gameOverCanvas.transform.Find("Restart").gameObject);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearScript : MonoBehaviour

{
    [SerializeField] CharacterController controller;
    public Vector3 dir;
    int dashSpeed = 15;
    float dashTime = .3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dash();
    }

    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            dir = (transform.forward * 1).normalized;
            controller.Move(dir * dashSpeed * Time.deltaTime);
            yield return null;
        }


    }

    void dash()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(DashCoroutine());

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private float destroyTimer = 1f;
    public AudioClip explosionSfx;
    void Start()
    {
       if(explosionSfx != null)
        {
            GetComponent<AudioSource>().PlayOneShot(explosionSfx);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, destroyTimer);
    }
}

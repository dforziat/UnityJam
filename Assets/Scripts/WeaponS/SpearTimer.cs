using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearTimer : MonoBehaviour
{
    public bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startTimer()
    {
        canShoot = false;
        StartCoroutine(timer());
        IEnumerator timer()
        {
            yield return new WaitForSeconds(2);
            canShoot = true;

        }
    }
}

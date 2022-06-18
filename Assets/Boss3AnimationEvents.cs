using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3AnimationEvents : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rifle;
    //Rifle stats
    Vector3 rifleRunPosition = new Vector3(1.2f, 0.56f, 0.67f);
    Vector3 rifleRunRotation = new Vector3(285, 90, 0);

    Vector3 rifleShootPosition = new Vector3(0.589999974f, 0.637000024f, 1.528f);
    Vector3 rifleShootRotation = new Vector3(293.405579f, 58.6852112f, 343.351044f);


    public GameObject saber;


    void Start()
    {
        saber.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void moveRifleShoot()
    {
        rifle.transform.localPosition = rifleShootPosition;
        rifle.transform.localEulerAngles = rifleShootRotation;
    }

    public void disableRifle()
    {
        rifle.SetActive(false);
    }

    private void enableRifle()
    {
        rifle.SetActive(true);
    }

    private void enableSaber()
    {
        saber.SetActive(true);
    }

    private void disableSaber()
    {
        saber.SetActive(false);
    }
}

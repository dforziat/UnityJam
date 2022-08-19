using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;


public class EpilogueCutscene : MonoBehaviour
{
    public PlayableDirector timeline;
    public GameObject GUI;


    // Start is called before the first frame update
    void Awake()
    {
        timeline = GetComponent<PlayableDirector>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timeline.Play();
            GUI.SetActive(false);


        }

    }

    public void weaponEquip()
    {
        GUI.SetActive(true);
    }
}

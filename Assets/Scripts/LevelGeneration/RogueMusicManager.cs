using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueMusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<AudioClip> musicList;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        int randomNum = Random.Range(0, musicList.Count);
        audioSource.PlayOneShot(musicList[randomNum]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

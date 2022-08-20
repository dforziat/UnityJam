using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EpilogueButton : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isActivated = false;
    public GameObject triggerBox;
    public GameObject endScreen;
    private Animator animator;
    public AudioClip buttonClip;
    private AudioSource audioSource;
    public PlayableDirector timeline1;
    public PlayableDirector timeline2;


    void Start()
    {
        animator = GetComponent<Animator>();
        //door.GetComponent<DoorProximityScript>().isUnlocked = false;
        audioSource = GetComponent<AudioSource>();
        triggerBox.SetActive(false);
        endScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate()
    {
        if (isActivated == false)
        {
            isActivated = true;
            animator.SetTrigger("activate");
            audioSource.PlayOneShot(buttonClip);
            timeline1.Stop();
            timeline2.Play();
            StartCoroutine(slightPause());

        }
    }

    IEnumerator slightPause()
    {
        
        //dan put code to start explosions here

        //player starts turning here
        yield return new WaitForSeconds(3f);

        //dialogue starts
        triggerBox.SetActive(true);

        //waits till dialogue is over and then rest tosses up end screen
        //increase the wait time to wait longer till game ends
        yield return new WaitForSeconds(13f);
        LevelManager.levelLoading = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endScreen.SetActive(true);


    }

    public void signalEndGame()
    {
         triggerBox.SetActive(true);
         StartCoroutine(slightPause());
    }
}

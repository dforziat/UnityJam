using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpilogueButton : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isActivated = false;
    public GameObject triggerBox;
    public GameObject endScreen;
    private Animator animator;
    public AudioClip buttonClip;
    private AudioSource audioSource;
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
            //door.GetComponent<DoorProximityScript>().isUnlocked = true;
            audioSource.PlayOneShot(buttonClip);
            triggerBox.SetActive(true);
            StartCoroutine(slightPause());
        }
    }

    IEnumerator slightPause()
    {
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        endScreen.SetActive(true);


    }
}

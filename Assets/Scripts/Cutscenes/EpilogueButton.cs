using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;

public class EpilogueButton : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isActivated = false;
    public GameObject GUI;
    public GameObject triggerBox;
    public GameObject endScreen;
    public GameObject mainMenuButton;
    private Animator animator;
    public AudioClip buttonClip;
    private AudioSource audioSource;
    public PlayableDirector timeline1;
    public PlayableDirector timeline2;
    public Transform explosionPoint;

    public GameObject explosion;
    private float explosionTime = 1f;


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
            GUI.SetActive(false);


        }
    }

    IEnumerator slightPause()
    {

        //dan put code to start explosions here
        explode();

        if (SteamManager.Initialized)
        {
            SteamUserStats.GetAchievement(SteamAchievementConstants.ART, out bool achievmentUnlocked);
            if (!achievmentUnlocked)
            {
                SteamScript.incrementPlatStat();
                SteamUserStats.SetAchievement(SteamAchievementConstants.ART);
                SteamUserStats.StoreStats();
            }

        }

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
        EventSystem.current.SetSelectedGameObject(mainMenuButton);

    }

    public void signalEndGame()
    {
         triggerBox.SetActive(true);
         StartCoroutine(slightPause());
    }

    public void explode()
    {
        float max = 1.5f;
        float min = -1.5f;
        float randomX = Random.Range(max, min);
        float randomY = Random.Range(max, min);
        float randomZ = Random.Range(max, min);
        Instantiate(explosion, new Vector3(explosionPoint.position.x + randomX, explosionPoint.position.y + randomY + 1f, explosionPoint.position.z), explosionPoint.transform.rotation);
        if(explosionTime > .3f)
        {
            explosionTime -= .1f;
        }
        if (!endScreen.activeInHierarchy)
        {
            Invoke("explode", explosionTime);
        }
    }
}

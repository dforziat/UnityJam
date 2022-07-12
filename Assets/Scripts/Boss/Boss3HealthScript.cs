using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3HealthScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip healthFillClip;
    public AudioClip healthShatterClip;

    public Animator hpBarAnimator;
    public Animator hpChunkAnimator;

    public RectTransform healthChunk;

    public Boss3Script bossScript;

    private int bossMaxHealth;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bossMaxHealth = bossScript.health;
        Debug.Log("Boss health: " + bossScript.health);
    }

    // Update is called once per frame
    void Update()
    {

        if(((float)bossScript.health / (float)bossMaxHealth <= .75) && !hpBarAnimator.GetBool("34"))
        {
            hpBarAnimator.SetBool("34", true);
            breakChunk();
        }
        if (((float)bossScript.health / (float)bossMaxHealth <= .5) && !hpBarAnimator.GetBool("12"))
        {
            hpBarAnimator.SetBool("12", true);
            breakChunk();
        }
        if (((float)bossScript.health / (float)bossMaxHealth <= .25) && !hpBarAnimator.GetBool("14"))
        {
            hpBarAnimator.SetBool("14", true);
            breakChunk();
        }
        if (((float)bossScript.health / (float)bossMaxHealth <= 0) && !hpBarAnimator.GetBool("0"))
        {
            hpBarAnimator.SetBool("0", true);
            breakChunk();
        }
    }

    public void playHealthFillClip()//Called By Animation Event
    {
        audioSource.PlayOneShot(healthFillClip);
    }

    public void breakChunk()
    {
        hpChunkAnimator.SetTrigger("explode");
        audioSource.PlayOneShot(healthShatterClip);

        healthChunk.anchoredPosition = new Vector2(healthChunk.anchoredPosition.x - 23, healthChunk.anchoredPosition.y);
    }


}

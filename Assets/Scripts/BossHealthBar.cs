using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip healthFillClip;
    public AudioClip healthShatterClip;

    public Animator hpBarAnimator;
    public Animator hpChunkAnimator;

    public RectTransform healthChunk;

    public BossScript bossScript;

    private int bossMaxHealth;
    private bool canBreak = true;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bossMaxHealth = bossScript.health;
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
        Debug.Log("Boss Hp Percentage: " + ((float)bossScript.health / (float)bossMaxHealth));
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

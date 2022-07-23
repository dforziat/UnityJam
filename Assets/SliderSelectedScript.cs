using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSelectedScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Image spriteHolder;
    public Sprite ogSprite;
    public Sprite selectedSprite;
    void Start()
    {
        //ogSprite = spriteHolder.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void swapSprite()
    {
        spriteHolder.sprite = selectedSprite;
    }

    public void displayOriginalSprite()
    {
        spriteHolder.sprite = ogSprite;
    }

}

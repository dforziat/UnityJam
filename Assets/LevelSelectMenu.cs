using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI actTitle;
    public GameObject act1Canvas;
    public GameObject act2Canvas;
    public GameObject act3Canvas;

    private AudioSource audioSource;

    private const string ACT_1 = "ACT 1";
    private const string ACT_2 = "ACT 2";
    private const string ACT_3 = "ACT 3";


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Awake()
    {
        act2Canvas.SetActive(false);
        act3Canvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveActRight()
    {
        audioSource.Play();
        switch (actTitle.text)
        {
            case ACT_1:
                act1Canvas.SetActive(false);
                act2Canvas.SetActive(true);
                actTitle.text = ACT_2;
                break;
            case ACT_2:
                act2Canvas.SetActive(false);
                act3Canvas.SetActive(true);
                actTitle.text = ACT_3;
                break;
            case ACT_3:
                act3Canvas.SetActive(false);
                act1Canvas.SetActive(true);
                actTitle.text = ACT_1;
                break;
        }
    }

    public void moveActLeft()
    {
        audioSource.Play();
        switch (actTitle.text)
        {
            case ACT_1:
                act1Canvas.SetActive(false);
                act3Canvas.SetActive(true);
                actTitle.text = ACT_3;
                break;
            case ACT_2:
                act2Canvas.SetActive(false);
                act1Canvas.SetActive(true);
                actTitle.text = ACT_1;
                break;
            case ACT_3:
                act3Canvas.SetActive(false);
                act2Canvas.SetActive(true);
                actTitle.text = ACT_2;
                break;
        }
    }


}

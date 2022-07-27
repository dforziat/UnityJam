using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestionMark : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update

    public GameObject panel;
    public bool pointerEntered = false;
    void Start()
    {
        panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //panel.SetActive(true);
        pointerEntered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //panel.SetActive(false);
        pointerEntered = false;
    }
}

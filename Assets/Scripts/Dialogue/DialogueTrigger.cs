using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void Start()
    {
        
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        TriggerDialogue();
        Destroy(this);
    }



}

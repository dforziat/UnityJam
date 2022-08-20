using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EpilogueSpecialDialogue : MonoBehaviour
{
    public Dialogue dialogue;
    public string scriptName;

    public void Start()
    {

    }

    public void TriggerDialogue()
    {

        GetComponentInParent<DialogueManager>().StartDialogue(dialogue);
        GetComponentInParent<DialogueManager>().triggerScript = scriptName;

    }
    private void OnTriggerEnter(Collider other)
    {
        TriggerDialogue();
        Destroy(this);    
    }
}

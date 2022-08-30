using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void Start()
    {
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

    }

}

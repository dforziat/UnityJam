using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public GameObject dialogueBox;

    private Queue<string> sentences;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueBox.SetActive(false);
        sentences = new Queue<string>();
    }

    private void Update()
    {

    }

    public void StartDialogue (Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentences();
    }

    public void DisplayNextSentences()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(.02f);
        }
        yield return new WaitForSeconds(2f);

        DisplayNextSentences();
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }


}

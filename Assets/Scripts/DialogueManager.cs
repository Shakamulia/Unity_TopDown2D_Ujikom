using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
   
    public static DialogueManager instance; // This is a singleton, which means that there is only one instance of this class

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines; // This is a queue that stores the dialogue lines

    public bool isDialogueActive = false; // This is a boolean that checks if the dialogue is active

    public float typingSpeed = 0.02f; // This is the speed of the typing effect

    public Animator animator; // This is the animator that controls the dialogue box

    // This method is called when the dialogue manager is enabled
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // This method is called when the dialogue manager is enabled
    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        animator.Play("Show");

        lines.Clear(); // Clear the queue

        foreach (DialogueLine dialogueLine in dialogue.DialogueLines)
        {
            lines.Enqueue(dialogueLine); // Add the lines to the queue
        }

        DisplayNextDialogueLine();
    }

    // This method is used to display the next dialogue line
    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    // This method is used to display the dialogue line with a typing effect
    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

    }

    // This method is called when the dialogue ends
    void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("Hide");
    }
}

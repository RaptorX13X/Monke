using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    //public Image characterIcon;
    public TextMeshProUGUI dialogueArea;
    public TextMeshProUGUI characterNameArea;
    public Image background;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.08f;

    public DialogueSO currentDialogue; 
    private DialogueLine currentLine;

    private bool isDialogueTyping;

    [SerializeField] private float timeBetweenLines;
    private float timeForLines;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
        timeForLines = timeBetweenLines;
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        currentDialogue = dialogue;
        isDialogueActive = true;

        lines.Clear();
        background.gameObject.SetActive(true);

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    private void Update()
    {
        if (!isDialogueActive) return;
        if (!isDialogueTyping && timeForLines >= 0.1f)
        {
            timeForLines -= Time.deltaTime;
        }
        else if (!isDialogueTyping && timeForLines <= 0.1f)
        {
            timeForLines = timeBetweenLines;
            DisplayNextDialogueLine();
        }
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            currentDialogue.isComplete = true;
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();

        //characterIcon.sprite = currentLine.character.icon;
        characterNameArea.text = currentLine.character.name;

        dialogueArea.gameObject.SetActive(true);
        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        isDialogueTyping = true;
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isDialogueTyping = false;
    }

    void EndDialogue()  
    {
        isDialogueActive = false;
        dialogueArea.text = "";
        background.gameObject.SetActive(false);
    }
}
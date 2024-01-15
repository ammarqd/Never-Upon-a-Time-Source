using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueDisplayer : MonoBehaviour
{
    // Singleton instance
    private static DialogueDisplayer _instance;

    // Reference to the singleton instance
    public static DialogueDisplayer Instance
    {
        get
        {
            if (_instance == null)
            {
                // If the instance is null, try to find an existing instance in the scene
                _instance = FindObjectOfType<DialogueDisplayer>();

                // If no instance is found, create a new GameObject with the DialogueDisplayer script attached
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("DialogueDisplayer");
                    _instance = singletonObject.AddComponent<DialogueDisplayer>();
                }
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    public DialogueObject currentDialogue;

    public float textSpeed;
    public bool isDialogueActive;
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            string text = currentDialogue.dialogueLines[index].dialogue;
            if (dialogueText.text == text)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = text;
            }
        }
    }

    public void setCurrentDialogue(DialogueObject dialogue) 
    {
        currentDialogue = dialogue;
    }

    public void StartDialogue()
    {
        if (isDialogueActive)
        {
            return;
        }

        isDialogueActive = true;
        index = 0;
        dialogueText.text = "";
        dialogueBox.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        string text = currentDialogue.dialogueLines[index].dialogue;
        foreach (char c in text.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < currentDialogue.dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueBox.SetActive(false);
            isDialogueActive = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private DialogueObject _doorDialogue;

    public DialogueDisplayer dialogueDisplayer;

    public string InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        if (dialogueDisplayer.isDialogueActive) {
            return false;
        }

        var inventory = interactor.GetComponent<Inventory>();

        if (inventory == null)
        {
            return false;
        } 

        if (inventory.HasKey)
        {
            Debug.Log("Door Unlocked: Opening Door!");
            return true;
        }

        Debug.Log("Door Locked: Cannot Open Door");
        dialogueDisplayer.setCurrentDialogue(getDialogue());
        dialogueDisplayer.StartDialogue();
        return true;
    }

    public DialogueObject getDialogue() {
        return _doorDialogue;
    }

}

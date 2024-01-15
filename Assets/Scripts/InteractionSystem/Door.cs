using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, IInteractable
{
    public LevelManager levelManager;
    public DialogueDisplayer dialogueDisplayer;
    public Animator animator1;
    public Animator animator2;
    public AudioSource audioSource;

    [SerializeField] private string _prompt;
    [SerializeField] private DialogueObject _doorDialogue;
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

            animator1.Play("Open Door");
            animator2.Play("Open Door");
            audioSource.enabled = true;

            StartCoroutine(WaitForAnimation());
            return true;
        }

        Debug.Log("Door Locked: Cannot Open Door");
        dialogueDisplayer.setCurrentDialogue(getDialogue());
        dialogueDisplayer.StartDialogue();
        return true;
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(animator1.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(animator2.GetCurrentAnimatorStateInfo(0).length);

        levelManager.LoadNextLevel();
    }

    public DialogueObject getDialogue() {
        return _doorDialogue;
    }

}
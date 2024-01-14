using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;

    public string InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
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
        return true;
    }
}

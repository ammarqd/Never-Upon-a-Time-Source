using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public Inventory inventory;

    public string InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        inventory.HasKey = true;
        
        return true;
    }
}

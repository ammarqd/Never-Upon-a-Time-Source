using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public Inventory inventory;
    public GameObject key;

    public string InteractionPrompt => _prompt;
    public bool Interact(Interactor interactor)
    {
        inventory.HasKey = true;
        Destroy(key);

        return true;

    }
}

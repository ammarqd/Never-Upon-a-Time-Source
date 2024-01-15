using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public DialogueDisplayer dialogueDisplayer;
    public GameObject endScreen;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndingScreen();
        }
    }
    public void EndingScreen()
    {
        if (!dialogueDisplayer.isDialogueActive)
        {
            endScreen.SetActive(true);
        }
    }
}
using UnityEngine;

public class TriggerNextLevel : MonoBehaviour
{
    [SerializeField] private LevelController levelController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelController.LoadNextLevel();
        }
    }
}

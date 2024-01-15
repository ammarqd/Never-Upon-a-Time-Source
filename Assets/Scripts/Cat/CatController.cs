using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CatController : MonoBehaviour
{
    public GameObject objectToMove; 
    public Transform target; 
    public Animator animator;
    public LevelManager levelManager;
    public float speed = 1.0f; 
    public float delayBeforeSceneChange = 3.0f;

    void Update()
    {
        if (objectToMove == null || target == null)
        {
            return;
        }

        float step = speed * Time.deltaTime;

        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, target.position, step);

        if (objectToMove.transform.position == target.position)
        {
            StartCoroutine(ChangeSceneAfterDelay());
        }
    }

    IEnumerator ChangeSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeSceneChange);

        levelManager.LoadNextLevel();
    }
}

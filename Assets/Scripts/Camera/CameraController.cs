using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public bool followOnXAxis = true; // Check this in the inspector to follow on X axis
    public bool followOnZAxis = false; // Check this in the inspector to follow on Z axis

    void LateUpdate()
    {
        float xPosition = followOnXAxis ? player.position.x : transform.position.x;
        float zPosition = followOnZAxis ? player.position.z : transform.position.z;

        Vector3 newPosition = new Vector3(xPosition, transform.position.y, zPosition);
        transform.position = newPosition;
    }
}

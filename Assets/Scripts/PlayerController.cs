using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public Vector2 moveValue;
    public float speed;
    public float rotationSpeed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }    
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        controller.Move(movement * speed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {
            // calculate the rotation the player should have to face towards the movement direction
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
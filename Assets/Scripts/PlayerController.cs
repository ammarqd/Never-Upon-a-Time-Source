using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    private Vector2 moveValue;
    private Vector3 playerVelocity;
    private bool isJumping;
    private bool isCrouching;

    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private float crouchSpeed = 2.5f;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private float jumpHeight = 1.0f;

    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private float standingHeight = 2.0f;

    [SerializeField]
    private float crouchingHeight;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (controller.isGrounded && value.isPressed)
        {
            isJumping = true;
        }
    }

    void OnCrouch(InputValue value)
    {
        isCrouching = value.isPressed;
        if (isCrouching)
        {
            controller.height = crouchingHeight;
        }
        else
        {
            controller.height = standingHeight;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

        float currentSpeed = isCrouching ? crouchSpeed : moveSpeed;

        // Handle horizontal movement
        controller.Move(movement * currentSpeed * Time.fixedDeltaTime);

        if (movement != Vector3.zero)
        {
            // calculate the rotation the player should have to face towards the movement direction
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Handle vertical movement (Jumping and Gravity)
        if (isJumping)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            isJumping = false;
        }

        playerVelocity.y += gravityValue * Time.fixedDeltaTime;
        controller.Move(playerVelocity * Time.fixedDeltaTime);

        // Reset player's vertical velocity when grounded
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    private Vector2 moveValue;
    private Vector3 playerVelocity;

    private bool isJumping;
    private bool isCrouching;
    private bool isSprinting;

    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private float crouchSpeed = 2.5f;

    [SerializeField]
    private float sprintSpeed = 10.0f;

    [SerializeField]
    private float rotationSpeed = 380.0f;

    [SerializeField]
    private float jumpHeight = 1.0f;

    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private float standingHeight = 5.5f;

    [SerializeField]
    private float crouchingHeight = 4.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("IsJumping");
        }
    }

    void OnCrouch(InputValue value)
    {
        isCrouching = value.isPressed;
        animator.SetBool("IsCrouching", isCrouching);
    }

    void OnSprint(InputValue value)
    {
        isSprinting = value.isPressed;
        animator.SetBool("IsSprinting", isSprinting);
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);
        movement.Normalize();

        float currentSpeed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : moveSpeed;

        // Handle horizontal movement
        controller.Move(movement * currentSpeed * Time.fixedDeltaTime);

        animator.SetFloat("Speed", movement.magnitude);

        if (movement != Vector3.zero)
        {
            // calculate the rotation the player should have to face towards the movement direction
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (isCrouching)
        {
            if (isSprinting)
            {
                isSprinting = false;
            }
            controller.height = crouchingHeight;
            controller.center = new Vector3(controller.center.x, crouchingHeight / 2, controller.center.z);
        }
        else
        {
            controller.height = standingHeight;
            controller.center = new Vector3(controller.center.x, standingHeight / 2, controller.center.z);
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
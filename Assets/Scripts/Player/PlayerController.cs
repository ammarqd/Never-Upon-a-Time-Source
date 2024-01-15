using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;

    public DialogueDisplayer dialogueDisplayer;

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
    private float rotationSpeed = 2000.0f;

    [SerializeField]
    private float jumpHeight = 1.0f;

    [SerializeField]
    private float gravityValue = -9.81f;

    [SerializeField]
    private float standingHeight = 1.4f;

    [SerializeField]
    private float crouchingHeight = 1.1f;

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
    }

    void FixedUpdate()
    {
        if (!dialogueDisplayer.isDialogueActive)
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            // Adjust movement to be relative to camera's orientation
            Vector3 movement = forward * moveValue.y + right * moveValue.x;

            bool isMoving = movement.magnitude > 0;

            isSprinting = isMoving && isSprinting;

            float currentSpeed = isSprinting ? sprintSpeed : isCrouching ? crouchSpeed : moveSpeed;

            // Handle horizontal movement
            controller.Move(movement * currentSpeed * Time.fixedDeltaTime);

            float speedValue;
            if (isSprinting)
            {
                speedValue = 1f;
            }
            else if (movement.magnitude > 0)
            {
                speedValue = 0.5f;
            }
            else
            {
                speedValue = 0f;
            }

            animator.SetFloat("Speed", speedValue, 0.2f, Time.deltaTime);

            float crouchSpeedValue = isCrouching ? (movement.magnitude > 0 ? 0.5f : 0f) : 0f;
            animator.SetFloat("Crouch", crouchSpeedValue, 0.1f, Time.deltaTime);

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
}
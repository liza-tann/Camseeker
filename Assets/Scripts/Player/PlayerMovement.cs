using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;         // Speed of forward/backward movement
    public float rotationSpeed = 180f;  // Degrees per second for left/right rotation
    public VariableJoystick joystick;   // Reference to the VariableJoystick

    private Animator animator;
    private CharacterController characterController;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get joystick input
        float verticalInput = joystick.Vertical;   // Forward/backward movement
        float horizontalInput = joystick.Horizontal; // Left/right rotation

        // Forward/backward movement
        Vector3 forwardMovement = transform.forward * verticalInput * moveSpeed;

        // Lock Y to 0
        forwardMovement.y = 0f;

        // Apply movement
        characterController.Move(forwardMovement * Time.deltaTime);

        // Left/right rotation
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);

        // Determine if the player is moving
        bool isMoving = Mathf.Abs(verticalInput) > 0.1f || Mathf.Abs(horizontalInput) > 0.1f;

        // Update the Animator parameter
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
        else
        {
            Debug.LogError("Animator component is missing or not assigned.");
        }

        // Debug
        Debug.Log("isMoving: " + animator.GetBool("isMoving"));
    }

    public void SetAnimator(Animator newAnimator)
    {
        animator = newAnimator;
    }
}

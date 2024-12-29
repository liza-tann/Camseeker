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

        // Animator logic
        bool isMoving = verticalInput != 0 || horizontalInput != 0;
        animator.SetBool("isMoving", isMoving);

        // Debug
        Debug.Log("isMoving: " + animator.GetBool("isMoving"));
    }
}

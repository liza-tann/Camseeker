using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;         // Speed for player movement
    public float rotationSpeed = 10f;   // Smooth rotation speed multiplier

    public Animator animator;           // Animator for player animations
    public Joystick joystick;           // Reference to the fixed joystick
    public CharacterController characterController; // CharacterController for movement

    // Start is called before the first frame update
    void Start()
    {
        // Get required components
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

        // Debug check for missing references
        if (joystick == null)
        {
            Debug.LogError("Joystick is not assigned in the Inspector.");
        }
        if (characterController == null)
        {
            Debug.LogError("CharacterController is not assigned or missing.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (joystick != null && characterController != null)
        {
            // Get joystick input
            float horizontalInput = joystick.Horizontal; // Left/right movement
            float verticalInput = joystick.Vertical;    // Forward/backward movement

            // Movement direction in local space
            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

            // Check if there's any movement
            if (movementDirection.magnitude >= 0.1f)
            {
                // Calculate target rotation based on joystick direction
                float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
                float smoothedAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);

                // Rotate player smoothly
                transform.rotation = Quaternion.Euler(0, smoothedAngle, 0);

                // Move the player in the desired direction
                Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
                characterController.Move(move * moveSpeed * Time.deltaTime);

                // Trigger "isMoving" animation
                animator.SetBool("isMoving", true);
            }
            else
            {
                // No movement, stop "isMoving" animation
                animator.SetBool("isMoving", false);
            }
        }
    }
}



// using UnityEngine;

// public class PlayerMovement1 : MonoBehaviour
// {
//     public float moveSpeed = 5f;         // Speed of forward/backward movement
//     public float rotationSpeed = 180f;  // Degrees per second for left/right rotation

//     public Animator animator;
//     public Joystick joystick;
//     public CharacterController characterController;

//     float verticalMove = 0f;
//     float horizontalMove = 0f;

//     // Start is called before the first frame update
//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         characterController = GetComponent<CharacterController>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // Movement input
//         // float verticalInput = Input.GetAxis("Vertical");   // Forward/backward movement (W/S)
//         // float horizontalInput = Input.GetAxis("Horizontal"); // Left/right rotation (A/D)
//         if (joystick.Horizontal >= .2f)
//         {
//             horizontalMove = moveSpeed;
//         }
//         else if (joystick.Horizontal <= -.2f)
//         {
//             horizontalMove = -moveSpeed;
//         }
//         else
//         {
//             horizontalMove = 0f;
//         }
//         verticalMove = joystick.Vertical * moveSpeed;
//         // horizontalMove = joystick.Horizontal * moveSpeed;

//         // Forward/backward movement
//         // Correct forward movement calculation
//         Vector3 forwardMovement = transform.forward * verticalMove;

//         // Apply movement
//         characterController.Move(forwardMovement * Time.deltaTime);

//         // Left/right rotation
//         float rotationAmount = horizontalMove * rotationSpeed * Time.deltaTime;
//         transform.Rotate(0, rotationAmount, 0);

//         // Animator logic
//         if (verticalMove != 0)
//         {
//             animator.SetBool("isMoving", true);
//         }
//         else
//         {
//             animator.SetBool("isMoving", false);
//         }

//         // Debug
//         Debug.Log("isMoving: " + animator.GetBool("isMoving"));
//     }
// }

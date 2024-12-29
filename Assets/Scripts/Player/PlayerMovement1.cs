// using UnityEngine;

// public class PlayerMovement1 : MonoBehaviour
// {
//     public float speed;
//     public float rotationSpeed;
//     public float jumpSpeed;
//     public float jumpButtonGracePeriod;

//     private Animator animator;
//     private CharacterController characterController;
//     private float ySpeed;
//     private float originalStepOffset;
//     private float? lastGroundedTime;
//     private float? jumpButtonPressedTime;

//     // Start is called before the first frame update
//     void Start()
//     {
//         animator = GetComponent<Animator>();
//         characterController = GetComponent<CharacterController>();
//         originalStepOffset = characterController.stepOffset;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         float horizontalInput = Input.GetAxis("Horizontal");
//         float verticalInput = Input.GetAxis("Vertical");

//         Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
//         float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
//         movementDirection.Normalize();

//         ySpeed += Physics.gravity.y * Time.deltaTime;

//         if (characterController.isGrounded)
//         {
//             lastGroundedTime = Time.time;
//         }

//         if (Input.GetButtonDown("Jump"))
//         {
//             jumpButtonPressedTime = Time.time;
//         }

//         if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
//         {
//             characterController.stepOffset = originalStepOffset;
//             ySpeed = -0.5f;

//             if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
//             {
//                 ySpeed = jumpSpeed;
//                 jumpButtonPressedTime = null;
//                 lastGroundedTime = null;
//             }
//         }
//         else
//         {
//             characterController.stepOffset = 0;
//         }

//         Vector3 velocity = movementDirection * magnitude;
//         velocity.y = ySpeed;

//         characterController.Move(velocity * Time.deltaTime);

//         if (movementDirection != Vector3.zero)
//         {
//             animator.SetBool("isMoving", true);
//             Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

//             transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
//         }
//         else
//         {
//             animator.SetBool("isMoving", false);
//         }
//         Debug.Log("isMoving: " + animator.GetBool("isMoving"));


//     }
// }

using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float moveSpeed = 5f;         // Speed of forward/backward movement
    public float rotationSpeed = 180f;  // Degrees per second for left/right rotation

    private Animator animator;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement input
        float verticalInput = Input.GetAxis("Vertical");   // Forward/backward movement (W/S)
        float horizontalInput = Input.GetAxis("Horizontal"); // Left/right rotation (A/D)

        // Forward/backward movement
        Vector3 forwardMovement = transform.forward * verticalInput * moveSpeed;

        // Apply movement
        characterController.Move(forwardMovement * Time.deltaTime);

        // Left/right rotation
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotationAmount, 0);

        // Animator logic
        if (verticalInput != 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // Debug
        Debug.Log("isMoving: " + animator.GetBool("isMoving"));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkAnimation : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isBackwardHash;
    int isTurnLeftHash;
    int isTurnRightHash;

    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isBackwardHash = Animator.StringToHash("isBackward");
        isTurnLeftHash = Animator.StringToHash("isTurnLeft");
        isTurnRightHash = Animator.StringToHash("isTurnRight");
    }

    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isBackward = animator.GetBool(isBackwardHash);
        bool isTurnLeft = animator.GetBool(isTurnLeftHash);
        bool isTurnRight = animator.GetBool(isTurnRightHash);

        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool turnLeftPressed = Input.GetKey("a");
        bool turnRightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // Update animation states for walking forward
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        // Update animation states for running
        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }

        // Update animation states for walking backward
        if (!isBackward && backwardPressed)
        {
            animator.SetBool(isBackwardHash, true);
        }
        if (isBackward && !backwardPressed)
        {
            animator.SetBool(isBackwardHash, false);
        }

        // Update animation states for turning left
        if (!isTurnLeft && turnLeftPressed)
        {
            animator.SetBool(isTurnLeftHash, true);
        }
        if (isTurnLeft && !turnLeftPressed)
        {
            animator.SetBool(isTurnLeftHash, false);
        }

        // Update animation states for turning right
        if (!isTurnRight && turnRightPressed)
        {
            animator.SetBool(isTurnRightHash, true);
        }
        if (isTurnRight && !turnRightPressed)
        {
            animator.SetBool(isTurnRightHash, false);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * 5f * Time.fixedDeltaTime;
        transform.Translate(movement);
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class walkAnimation : MonoBehaviour
// {
//     Animator animator;

//     public float walkSpeed = 3f; // Movement speed for forward/backward
//     public float turnSpeed = 2f; // Rotation speed for turning

//     void Start()
//     {
//         animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         // Get input for forward/backward movement
//         float targetForwardInput = Input.GetKey("w") ? 1f : (Input.GetKey("s") ? -1f : 0f);

//         // Get input for turning
//         float turnInput = Input.GetKey("a") ? -1f : (Input.GetKey("d") ? 1f : 0f);

//         // Smoothly interpolate Forward parameter to avoid sudden changes
//         float forwardInput = Mathf.Lerp(animator.GetFloat("Forward"), targetForwardInput, Time.deltaTime * 5f);

//         // Update Animator parameters
//         animator.SetFloat("Forward", forwardInput);
//         animator.SetFloat("Turn", turnInput);

//         // Handle turning (rotation)
//         if (turnInput != 0f)
//         {
//             transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime);
//         }

//         // Handle forward/backward movement
//         if (forwardInput != 0f)
//         {
//             transform.Translate(Vector3.forward * forwardInput * walkSpeed * Time.deltaTime);
//         }
//     }
// }

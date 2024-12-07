using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkAnimation : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    //Rigidbody rb; // Reference to Rigidbody for movement

    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    void Start()
    {
        animator = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody>(); // Get Rigidbody
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey("w");
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // Update animation states
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
        if (Input.GetKey("w"))
        {
            Debug.Log("W key pressed");
        }


    }

    // void FixedUpdate()
    // {
    //     // Handle movement
    //     float speed = animator.GetBool("isRunning") ? runSpeed : walkSpeed;
    //     Vector3 movement = Vector3.zero;

    //     if (Input.GetKey("w"))
    //     {
    //         movement += transform.forward;
    //     }


    //     movement = movement.normalized * speed * Time.fixedDeltaTime;
    //     rb.MovePosition(rb.position + movement);
    // }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * 5f * Time.fixedDeltaTime;
        transform.Translate(movement);
    }




}

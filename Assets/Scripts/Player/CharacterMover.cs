using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float moveSpeed = 5f;        // Speed for movement
    public float rotationSpeed = 200f; // Speed for rotation

    void Update()
    {
        // Input for forward/backward movement
        float moveInput = Input.GetAxis("Vertical");

        // Input for left/right rotation
        float rotationInput = Input.GetAxis("Horizontal");

        // Translate the character forward or backward
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);

        // Rotate the character left or right
        transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
    }
}

// using UnityEngine;
// public class OpenDoor : MonoBehaviour
// {
//     private bool isOpen = false;
//     private Quaternion closedRotation;
//     private Quaternion openRotation;
//     public float rotationSpeed = 2f; // Speed of door rotation
//     void Start()
//     {        // Store the initial rotation of the door
//         closedRotation = transform.rotation;
//         // Define the rotation when the door is open        
//         openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90, 0)); // Adjust as needed
//     }
//     void OnMouseDown()
//     {
//         // Toggle the door state on click        
//         isOpen = !isOpen;
//     }
//     void Update()
//     {        // Rotate the door smoothly towards the target rotation
//         Quaternion targetRotation = isOpen ? openRotation : closedRotation;
//         transform.rotation = Quaternion.Lerp(
//             transform.rotation,
//             targetRotation, Time.deltaTime * rotationSpeed
//         );
//     }
// }

using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    public float rotationSpeed = 2f; // Speed of door rotation


    void Start()
    {
        // Store the initial rotation of the door
        closedRotation = transform.rotation;

        // Define the rotation when the door is open        
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 90, 0));
    }

    void OnMouseDown()
    {
        // Toggle the door state on click        
        isOpen = !isOpen;
    }

    void Update()
    {
        // Rotate the door smoothly towards the target rotation
        Quaternion targetRotation = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * rotationSpeed
        );
    }

}
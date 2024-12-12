using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToScene1 : MonoBehaviour
{
    public string sceneName = "groundFloor";  // The name of the scene to load

    void Update()
    {
        // Check if the user clicks on the capsule
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera to the mouse position
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object clicked has the tag "Finish"
                if (hit.collider.CompareTag("Finish")) // Check if the capsule has been clicked
                {
                    SceneManager.LoadScene(sceneName); // Load Scene1
                }
            }
        }
    }
}

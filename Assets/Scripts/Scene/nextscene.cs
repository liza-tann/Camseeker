// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class nextscene : MonoBehaviour
// {
//     public string scenename;

//     void Update()
//     {
//         // Check if the user clicks on the cube
//         if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
//         {
//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera to the mouse position
//             RaycastHit hit;

//             if (Physics.Raycast(ray, out hit))
//             {
//                 if (hit.collider.CompareTag("Player")) // Check if the cube has been clicked
//                 {
//                     // Check if the scenename is not empty before trying to load the scene
//                     if (!string.IsNullOrEmpty(scenename))
//                     {
//                         SceneManager.LoadScene(scenename); // Load the next scene
//                     }
//                     else
//                     {
//                         Debug.LogError("Scene name is not set or is empty.");
//                     }
//                 }
//             }
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string scenename;

    void Update()
    {
        // Check if the user clicks on the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera to the mouse position
            RaycastHit hit;

            // Perform raycast, ensure it hits objects
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast hit: " + hit.transform.name); // Log the name of the object hit by the raycast

                // Check if the clicked object has the "Player" tag
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.Log("Player clicked!"); // Log confirmation that the player was clicked

                    if (!string.IsNullOrEmpty(scenename)) // Check if scenename is set
                    {
                        Debug.Log("Loading scene: " + scenename); // Log the name of the scene being loaded
                        SceneManager.LoadScene(scenename); // Load the specified scene
                    }
                    else
                    {
                        Debug.LogError("Scene name is not set or is empty.");
                    }
                }
                else
                {
                    Debug.Log("Clicked on something other than the player.");
                }
            }
            else
            {
                Debug.Log("Raycast did not hit any object.");
            }
        }
    }
}



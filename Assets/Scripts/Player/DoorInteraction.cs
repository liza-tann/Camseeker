using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteraction : MonoBehaviour
{
    public string sceneToLoad = "RiddleScene"; // The scene you want to load

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player")) // Make sure the player object has the "Player" tag
        {
            Debug.Log("Player entered the door area! Loading scene...");
            LoadGame(sceneToLoad);
        }
    }

    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

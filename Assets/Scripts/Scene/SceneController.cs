using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Animator animator; // Animator for scene transition
    public Vector3 playerPosition; // Player's position to store between scenes
    public int score; // Score to store between scenes

    private string targetSceneName;

    public void FadeToScene(string sceneName)
    {
        targetSceneName = sceneName; // Store the target scene name
        animator.SetTrigger("FadeOut"); // Trigger fade-out animation
        StartCoroutine(WaitAndLoadScene()); // Start the coroutine to delay scene load
    }

    private IEnumerator WaitAndLoadScene()
    {
        // Wait for the fade-out animation to complete
        float fadeDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(fadeDuration);

        // Load the new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive);

        // Wait for the new scene to load
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Get references to the current and new scenes
        Scene currentScene = SceneManager.GetActiveScene();
        Scene newScene = SceneManager.GetSceneByName(targetSceneName);

        // Set the new scene as active
        SceneManager.SetActiveScene(newScene);

        // Deactivate all GameObjects in the current scene
        foreach (GameObject obj in currentScene.GetRootGameObjects())
        {
            obj.SetActive(false);
        }

        // Optionally unload the previous scene if it's no longer needed
        SceneManager.UnloadSceneAsync(currentScene);
    }

    public void SetPlayerState(Vector3 position, int newScore)
    {
        playerPosition = position;
        score = newScore;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    public int GetScore()
    {
        return score;
    }
}

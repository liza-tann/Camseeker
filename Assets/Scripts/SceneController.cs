using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;

    public Image fadeOverlay;
    public float transitionDuration = 1f;

    public string currentScene;
    public string targetScene;

    // list of globel scenes
    public List<string> GlobalScenes = new List<string> { "groundFloor" };

    private Dictionary<string, object> savedStates = new Dictionary<string, object>();

    [System.Serializable]
    public class SceneState
    {
        public Vector3 playerPosition;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (fadeOverlay != null)
            {
                fadeOverlay.color = new Color(0, 0, 0, 0); // Fully transparent at the start
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.Instance.SwitchToScene(targetScene);
        }
    }

    public void SwitchToScene(string newScene)
    {
        StartCoroutine(TransitionToScene(newScene));
    }

    private IEnumerator TransitionToScene(string newScene)
    {
        yield return StartCoroutine(Fade(1));

        SaveSceneState(SceneManager.GetActiveScene().name);
        // set current scene inactive
        string currentSceneName = SceneManager.GetActiveScene().name;
        if (!IsGlobalScene(currentSceneName) && !IsSceneNeeded(currentSceneName))
        {
            SceneManager.UnloadSceneAsync(currentSceneName);
        }

        // asynchronous scene loading
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newScene));

        // Restore the state of the new scene
        RestoreSceneState(newScene);

        // Start fade-in effect
        yield return StartCoroutine(Fade(0));

        // unload the previous scene
        if (!IsGlobalScene(currentSceneName) && !IsSceneNeeded(currentSceneName))
        {
            SceneManager.UnloadSceneAsync(currentSceneName);
        }
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeOverlay.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / transitionDuration);
            fadeOverlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeOverlay.color = new Color(0, 0, 0, targetAlpha);
    }

    // check if scene needed
    public static bool IsSceneNeeded(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (!scene.isLoaded)
        {
            Debug.Log($"Scene '{sceneName}' is not loaded.");
            return false;
        }

        // Check if the scene is a global/persistent scene
        if (IsGlobalScene(sceneName))
        {
            Debug.Log($"Scene '{sceneName}' is a global scene and cannot be unloaded.");
            return true;
        }

        // Check if there is any active dependency in the scene
        if (HasActiveDependencies(scene))
        {
            Debug.Log($"Scene '{sceneName}' has active dependencies and cannot be unloaded.");
            return true;
        }

        // Check if there is any persistent data in the scene
        if (HasPersistentData(scene))
        {
            Debug.Log($"Scene '{sceneName}' contains persistent data and cannot be unloaded.");
            return true;
        }
        // Scene can be unloaded
        return false;
    }

    private static bool IsGlobalScene(string sceneName)
    {
        return Instance != null && Instance.GlobalScenes.Contains(sceneName);
    }

    private static bool HasActiveDependencies(Scene scene)
    {
        foreach (GameObject rootObj in scene.GetRootGameObjects())
        {
            // Check if any object in the scene is still active
            if (rootObj.activeSelf)
            {
                // Optional: Check if it has specific components (e.g., GameManager, PlayerController)
                if (rootObj.GetComponent<MonoBehaviour>() != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private static bool HasPersistentData(Scene scene)
    {
        foreach (GameObject rootObj in scene.GetRootGameObjects())
        {
            // Look for objects tagged as persistent
            if (rootObj.CompareTag("Persistent"))
            {
                return true;
            }
        }
        return false;
    }

    public void SaveSceneState(string sceneName)
    {
        if (savedStates.ContainsKey(sceneName))
        {
            Debug.Log($"State for scene '{sceneName}' already saved.");
            return;
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("Player not found in the scene. Cannot save state.");
            return;
        }

        SceneState stateData = new SceneState
        {
            playerPosition = GameObject.FindWithTag("Player").transform.position
        };

        savedStates[sceneName] = stateData;

        Debug.Log($"State for scene '{sceneName}' saved.");
    }

    public void RestoreSceneState(string sceneName)
    {
        if (savedStates.TryGetValue(sceneName, out object stateData))
        {
            SceneState sceneState = stateData as SceneState;
            if (sceneState != null)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    player.transform.position = sceneState.playerPosition;
                }
                Debug.Log($"State for scene '{sceneName}' restored.");
            }
        }
        else
        {
            Debug.Log($"No saved state found for scene '{sceneName}'.");
        }
    }
}

using UnityEngine;

public class ContinuousAudio : MonoBehaviour
{
    private static ContinuousAudio instance;

    void Awake()
    {
        // If there is already an instance of this object, destroy this one to avoid duplicates
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            // Make the music persist across scenes
            DontDestroyOnLoad(gameObject);
        }
    }
}

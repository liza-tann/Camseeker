using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RealTimeLightingPostProcessing : MonoBehaviour
{
    public PostProcessProfile postProcessingProfile; // Assign your Post-Processing profile here

    private PostProcessVolume _postProcessVolume;
    private ColorGrading _colorGrading;
    private Bloom _bloom;

    void Start()
    {
        // Check if the profile is assigned
        if (postProcessingProfile == null)
        {
            Debug.LogError("Post-Processing Profile is not assigned!");
            return;
        }

        // Create a Post-Process Volume and assign the profile
        _postProcessVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, postProcessingProfile.settings.ToArray());

        // Check for specific effects in the profile
        if (!postProcessingProfile.TryGetSettings(out _colorGrading))
        {
            Debug.LogWarning("ColorGrading is not part of the Post-Processing Profile. Adding it dynamically.");
            _colorGrading = postProcessingProfile.AddSettings<ColorGrading>();
        }

        if (!postProcessingProfile.TryGetSettings(out _bloom))
        {
            Debug.LogWarning("Bloom is not part of the Post-Processing Profile. Adding it dynamically.");
            _bloom = postProcessingProfile.AddSettings<Bloom>();
        }
    }

    void Update()
    {
        if (_colorGrading != null)
        {
            // Example: Adjust color grading based on time of day
            float time = Mathf.PingPong(Time.time, 1f); // Simulate a time-based value
            _colorGrading.temperature.value = Mathf.Lerp(-10f, 10f, time);
            _colorGrading.saturation.value = Mathf.Lerp(0f, 50f, time);
        }

        if (_bloom != null)
        {
            // Example: Adjust bloom intensity based on some real-time condition
            float intensity = Mathf.PingPong(Time.time * 0.5f, 10f);
            _bloom.intensity.value = intensity;
        }
    }

    private void OnDestroy()
    {
        // Clean up the volume when the script is destroyed
        RuntimeUtilities.DestroyVolume(_postProcessVolume, true, true);
    }
}

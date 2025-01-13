using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Camera))]
public class MinimapCameraSetup : MonoBehaviour
{
    [Header("Minimap Settings")]
    public Camera mainCamera; // Reference to the main camera
    public RenderTexture minimapRenderTexture; // RenderTexture for the minimap
    public Vector2 minimapSize = new Vector2(256, 256); // Size of the minimap render texture

    private Camera minimapCamera;

    void Start()
    {
        // Ensure the main camera is assigned
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera is not assigned!");
            return;
        }

        // Create a new RenderTexture if none is assigned
        if (minimapRenderTexture == null)
        {
            minimapRenderTexture = new RenderTexture((int)minimapSize.x, (int)minimapSize.y, 16);
        }

        // Initialize the minimap camera
        minimapCamera = GetComponent<Camera>();
        minimapCamera.targetTexture = minimapRenderTexture;
        minimapCamera.orthographic = true; // Set to orthographic for top-down view
        minimapCamera.clearFlags = CameraClearFlags.SolidColor;
        minimapCamera.backgroundColor = Color.black; // Background color for minimap

        // Adjust the minimap camera position and rotation
        minimapCamera.transform.position = mainCamera.transform.position + new Vector3(0, 50, 0);
        minimapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);

        // Setup Universal Additional Camera Data
        var additionalCameraData = minimapCamera.GetUniversalAdditionalCameraData();
        if (additionalCameraData != null)
        {
            additionalCameraData.renderShadows = false; // Disable shadows for performance
            additionalCameraData.requiresColorOption = CameraOverrideOption.Off;
            additionalCameraData.requiresDepthOption = CameraOverrideOption.Off;
        }
    }

    void OnValidate()
    {
        // Automatically resize the RenderTexture when minimap size is updated
        if (minimapRenderTexture != null && (minimapRenderTexture.width != (int)minimapSize.x || minimapRenderTexture.height != (int)minimapSize.y))
        {
            minimapRenderTexture.Release();
            minimapRenderTexture.width = (int)minimapSize.x;
            minimapRenderTexture.height = (int)minimapSize.y;
            minimapRenderTexture.Create();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    public Transform playerCamera; // Reference to the camera or player
    public float activationDistance = 50f; // Maximum distance to activate objects
    public GameObject[] sceneAssets; // Array of assets to manage

    private void Start()
    {
        // If no specific assets are assigned, find all objects tagged "SceneAsset"
        if (sceneAssets.Length == 0)
        {
            sceneAssets = GameObject.FindGameObjectsWithTag("SceneAsset");
        }
    }

    private void Update()
    {
        foreach (GameObject asset in sceneAssets)
        {
            if (asset == null) continue;

            // Calculate distance for parent object
            float distance = Vector3.Distance(playerCamera.position, asset.transform.position);
            bool shouldBeActive = distance <= activationDistance;

            // Activate or deactivate parent
            asset.SetActive(shouldBeActive);

            // Optionally manage children separately
            if (shouldBeActive)
            {
                foreach (Transform child in asset.transform)
                {
                    float childDistance = Vector3.Distance(playerCamera.position, child.position);
                    child.gameObject.SetActive(childDistance <= activationDistance / 2); // Fine control
                }
            }
        }
    }
}


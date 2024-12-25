using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadResource(string resourcePath, System.Action<GameObject> callback)
    {
        StartCoroutine(LoadResourceAsync(resourcePath, callback));
    }

    private IEnumerator LoadResourceAsync(string resourcePath, System.Action<GameObject> callback)
    {
        ResourceRequest request = Resources.LoadAsync<GameObject>(resourcePath);
        yield return request;

        if (request.asset != null)
        {
            callback(Instantiate((GameObject)request.asset));
        }
    }

    public void UnloadUnusedResources()
    {
        Resources.UnloadUnusedAssets();
    }
}


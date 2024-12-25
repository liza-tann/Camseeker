using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    private Dictionary<string, Queue<GameObject>> objectPools = new Dictionary<string, Queue<GameObject>>();

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

    public void CreatePool(string poolName, GameObject prefab, int size)
    {
        if (!objectPools.ContainsKey(poolName))
        {
            objectPools[poolName] = new Queue<GameObject>();

            for (int i = 0; i < size; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objectPools[poolName].Enqueue(obj);
            }
        }
    }

    public GameObject GetObjectFromPool(string poolName)
    {
        if (objectPools.ContainsKey(poolName) && objectPools[poolName].Count > 0)
        {
            GameObject obj = objectPools[poolName].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        return null;
    }

    public void ReturnObjectToPool(string poolName, GameObject obj)
    {
        obj.SetActive(false);
        if (objectPools.ContainsKey(poolName))
        {
            objectPools[poolName].Enqueue(obj);
        }
    }
}

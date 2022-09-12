using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPooling : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;    
    public static AmmoPooling instanse;
    public void Spawning(Transform parent , List<GameObject> po)
    {
        pooledObjects = new List<GameObject>();
        pooledObjects = po;
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool , parent);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject GetPooledObject(List<GameObject> po)
    {
        pooledObjects = po;
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    private void Awake()
    {
        instanse = this;
    }
}

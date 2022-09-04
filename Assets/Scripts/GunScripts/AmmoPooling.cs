using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPooling : MonoBehaviour
{
    public static AmmoPooling instanse;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    private void Awake()
    {
        instanse = this;
    }
    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }
    public GameObject GetPooledObject()
    {
        //1
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            //2
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        //3   
        return null;
    }
}

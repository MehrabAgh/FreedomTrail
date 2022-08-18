using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetect : MonoBehaviour
{
    public bool _buyLoaded;
    public GameObject Lock;

    private void Start()
    {
        if(gameObject.name != "RifleBtn")
        Lock = transform.Find("Lock").gameObject;
    }
    private void Update()
    {
        var x = PlayerPrefs.GetString(gameObject.name);
        if (x == "Unlock")
        {
            _buyLoaded = true;
        }
        if (_buyLoaded)
        {
            foreach (Transform item in transform)
            {
                item.gameObject.SetActive(true);
            }
            Lock.SetActive(false);
        }
    }

}

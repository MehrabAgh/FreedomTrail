using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    public GameObject[] virtualCameras;
    public static CameraManagement instance;

    private void Awake()
    {
        instance = this;
    }  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverCharacter : MonoBehaviour
{
    public List<GameObject> Covers;

    public void OnEnableCovers()
    {
        foreach (GameObject item in Covers)
        {
            item.SetActive(true);
        }
    }
    public void OnDisableCovers()
    {
        foreach (GameObject item in Covers)
        {
            item.SetActive(false);
        }
    }
}

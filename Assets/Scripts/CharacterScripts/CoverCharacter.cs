using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverCharacter : MonoBehaviour
{
    public List<Collider> Covers;

    public void OnEnableCovers()
    {
        foreach (Collider item in Covers)
        {
            item.enabled = true;
        }
    }
    public void OnDisableCovers()
    {
        foreach (Collider item in Covers)
        {
            item.enabled = false;
        }
    }
}

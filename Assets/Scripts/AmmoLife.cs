using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLife : MonoBehaviour
{
    public int Damge;
    private void Start()
    {
        Destroy(this.gameObject, 2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoLife : MonoBehaviour
{
    public int Damage;
    private void Destroyed()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Invoke("Destroyed", 0.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

   // public GameObject[] enemies;
    public Rigidbody car;
    public HeliMovement heli;

    private void Start()
    {
        if(car)
            car.useGravity = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.transform.CompareTag("Player"))
        {
            if(car)
            {
                car.useGravity = true;
            }
            if(heli)
            {
                // figure it out LATER :)
            }

            Destroy(gameObject);
        }
    }

}

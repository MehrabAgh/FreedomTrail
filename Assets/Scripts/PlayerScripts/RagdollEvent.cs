using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollEvent
{
    public static void OnLive(Collider[] rigColliders, Rigidbody[] rigRigidbodies)
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (Collider col in rigColliders)
        {
            col.enabled = false;
        }
    }
    public static void OnDeath(Collider[] rigColliders, Rigidbody[] rigRigidbodies)
    {
        foreach (Rigidbody rb in rigRigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider col in rigColliders)
        {
            col.enabled = true;
        }
    }
}

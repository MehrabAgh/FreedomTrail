using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurrent : MonoBehaviour
{
    public Transform turrent;
    private Transform barrel;
    private Transform target;

    public ParticleSystem cannonShot;
    private bool targetInView = false;

    public float turSpeed = 5;

    private void Start()
    {
        target = GetComponent<EnemyCarTargetController>().player;
        barrel = turrent.GetChild(0);
    }

    private void Update()
    {
        Vector3 dir = target.position - turrent.position;
        var angle = Vector3.SignedAngle(dir, -turrent.up, Vector3.up);
        targetInView = Vector3.Angle(turrent.up, dir) <= 5;
        angle = angle < -5 ? -1 : 1;



       // var rotDir = (((Vector3.Angle(turrent.transform.forward, dir.normalized) - turrent.eulerAngles.z) + 360) % 360) > 180 ? -1 : 1;
       // turrent.transform.rotation = Quaternion.RotateTowards(turrent.transform.rotation, Quaternion.Euler(dir), Time.deltaTime);
       // turrent.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(turrent.up, dir, 5 * Time.deltaTime, 0));
       // turrent.transform.rotation = Quaternion.FromToRotation(turrent.transform.up, dir);
       // turrent.rotation = Quaternion.Lerp(turrent.rotation, Quaternion.Euler(cross), Time.deltaTime * turSpeed);
        turrent.transform.Rotate(Vector3.forward, Vector3.Angle(transform.position, target.position) * turSpeed * angle * Time.deltaTime);

        if (targetInView)
            cannonShot.Play();
    }
}

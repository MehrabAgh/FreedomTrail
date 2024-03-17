using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchingProjectiles : MonoBehaviour
{

    public Transform TargetObject , ThisObject;
    [Range(20.0f, 75.0f)] public float LaunchAngle;
    public int resolution = 30;
    public float curveHight = 25;
    public float gravity = -18;    
    private Rigidbody rigid;

    public static launchingProjectiles ins;

    private void Awake()
    {
        ins = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Launch();
    }
    public void SetValueAttack()
    {
        rigid = ThisObject.GetComponent<Rigidbody>();
    }
    public float DistanceTarget()
    {
        return Vector3.Distance(ThisObject.position, TargetObject.position);
    }
    public void Launch()
    {
        SetValueAttack();
        rigid.constraints = RigidbodyConstraints.None;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
        ThisObject.GetComponent<Collider>().isTrigger = false;
        Physics.gravity = Vector3.up * gravity;
        rigid.velocity = CalculateLaunchData().initialVelocity;        
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = TargetObject.position.y - ThisObject.position.y;
        Vector3 displacementXZ = new Vector3(TargetObject.position.x - ThisObject.position.x, 0, TargetObject.position.z - ThisObject.position.z);
        float time = Mathf.Sqrt(-2 * curveHight / gravity) + Mathf.Sqrt(2 * (displacementY - curveHight) / gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * curveHight);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }
    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
}



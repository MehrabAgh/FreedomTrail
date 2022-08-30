using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

public class EnemyManager : MonoBehaviour
{
    public Transform Target ,heliGun;
    public Transform[] PivGun;
    public GameObject Ammo, Ammo2;
    private bool waiting;
    public float Power = 1500, DelayStart;
    private float Delay, attackDist = 25;
    public Transform Bone;
    private void Start()
    {
        Target = FindObjectOfType<PlayerMovement>().transform;
    }
    private void LateUpdate()
    {
        if (GetComponent<HeliMovement>())
        {
            TargetLook(Target, heliGun); 
        }
        else
        {
            TargetLook(Target, Bone);
        }
     
    }
    private void Update()
    {
        if (Mathf.Abs(Target.position.magnitude - transform.position.magnitude) > attackDist)
            return;
        AttackShoot();
    }
    public void TargetLook(Transform Target , Transform piv)
    {
       piv.transform.LookAt(Target);
    }
    public void AttackShoot()
    {
        if (!GameManager.ins._isEndGame && !GameManager.ins._isPause) {
            if (waiting)
            {
                Delay -= Time.deltaTime;
                if (Delay <= 0)
                {
                    waiting = false;
                }
            }
            if (!waiting)
            {
                for (int i = 0; i < PivGun.Length; i++)
                {
                    Ammo2 = Instantiate(Ammo, PivGun[i].position, PivGun[i].rotation);
                    Ammo2.GetComponent<Rigidbody>().AddForce(Ammo2.transform.forward * Power);
                }
                Delay = DelayStart;
                waiting = true;
            }
        }
    }
}

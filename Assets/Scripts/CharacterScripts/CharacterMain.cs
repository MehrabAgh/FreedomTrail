using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class CharacterMain : MonoBehaviour
    {
        public MainGun gun;
        [HideInInspector] public GunResponse gunRes;
        public Transform parentAmmo;
        [HideInInspector] public Collider[] rigColliders;
        public Car myCar;
        [HideInInspector] public Rigidbody[] rigRigidbodies;
        [HideInInspector] public HealthCharacter myhealth;
        [HideInInspector] public FullBodyBipedIK ikComponent;
        [HideInInspector] public Animator Anim;
        public Transform HandLeft, HandRight;

        private void Awake()
        {
            rigColliders = GetComponentsInChildren<Collider>();
            rigRigidbodies = GetComponentsInChildren<Rigidbody>();
            gun = GetComponent<MainGun>();
            Anim = GetComponent<Animator>();
            ikComponent = GetComponent<FullBodyBipedIK>();
            myhealth = GetComponent<HealthCharacter>();
            myCar = GetComponentInParent<Car>();
        }   
    }
}
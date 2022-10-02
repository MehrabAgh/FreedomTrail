using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class EnemyState : MonoBehaviour
    {
        public enum enemystate
        {
            ATTACK,
            COVER,
            DEATH,
            RELOAD
        }
        public static enemystate enemyState { get; set; }

        private MainGun mygun;
        private Animator Anim;
        private FullBodyBipedIK ikComponent;
        private Collider[] rigColliders;
        private Rigidbody[] rigRigidbodies;
        private Car GetCar;
        private bool _dieWork;
        public EnemyState(FullBodyBipedIK ikComp,
            MainGun gun, Animator anim, Collider[] RigColliders,
            Rigidbody[] RigRigidbodies , Car myCar)
        {
            GetCar = myCar; 
            rigColliders = RigColliders;
            rigRigidbodies = RigRigidbodies;
            mygun = gun;
            Anim = anim;
            ikComponent = ikComp;
            RagdollEvent.OnLive(RigColliders, RigRigidbodies);
        }
        public void EventStates(enemystate state)
        {
            switch (state)
            {
                case enemystate.ATTACK:                 
                    mygun.GetGunModel().transform.SetParent(mygun.HandPos);
                    mygun.SetupTransformGun.Invoke();
                    ikComponent.enabled = true;
                    Anim.SetBool("isCover", false);
                    Anim.SetBool("isReload", false);
                    Anim.SetBool(mygun.GetAnimationClip(), true);
                    Anim.SetLayerWeight(1, 1);
                    break;
                case enemystate.COVER:
                    mygun.GetGunModel().transform.SetParent(mygun.HandPos);
                    mygun.GetGunModel().transform.localPosition = Vector3.zero;
                    mygun.GetGunModel().transform.localRotation = Quaternion.identity;
                    Anim.SetBool("isCover", true);
                    ikComponent.enabled = false;
                    Anim.SetLayerWeight(1, 0);
                    break;
                case enemystate.DEATH:
                    if (!_dieWork)
                    {
                        ScoreManager.instance.KillCounter(1);
                        Anim.enabled = false;
                        ikComponent.enabled = false;
                        mygun.GetGunModel().SetActive(false);
                        mygun.Ammos.Clear();
                        GetCar.Die();
                        RagdollEvent.OnDeath(rigColliders, rigRigidbodies);
                        _dieWork = true;
                    }
                    break;
                case enemystate.RELOAD:
                    mygun.GetGunModel().transform.SetParent(mygun.HandPos);
                    mygun.GetGunModel().transform.localPosition = Vector3.zero;
                    mygun.GetGunModel().transform.localRotation = Quaternion.identity;
                    Anim.SetLayerWeight(2, 1);
                    Anim.SetBool("isCover", true);
                    Anim.SetBool("isReload", true);
                    ikComponent.enabled = false;
                    break;
                default:
                    break;
            }
        }    
    }
}

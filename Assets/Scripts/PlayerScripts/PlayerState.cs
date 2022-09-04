using RootMotion.FinalIK;
using System.Collections;
using System;
using UnityEngine;

namespace Vino.Devs
{
    public class PlayerState
    {
        public enum playerState
        {
            IDLE,
            ATTACK,
            COVER,
            DEATH,
            RELOAD
        }
        public static playerState myState { get; set; }

        private mainGun mygun;
        private Animator Anim;
        private Collider[] rigColliders;
        private Rigidbody[] rigRigidbodies;
        private FullBodyBipedIK ikComponent;

        public PlayerState(FullBodyBipedIK ikComp, mainGun gun, Animator anim, Collider[] RigColliders, Rigidbody[] RigRigidbodies)
        {
            rigColliders = RigColliders;
            rigRigidbodies = RigRigidbodies;
            mygun = gun;
            Anim = anim;
            ikComponent = ikComp;
           // RagdollEvent.OnLive(rigColliders, rigRigidbodies);
        }

        public void EventStates(playerState state)
        {
            switch (state)
            {
                case playerState.IDLE:
                 //   RagdollEvent.OnLive(rigColliders, rigRigidbodies);
                    break;

                case playerState.ATTACK:
                    mygun.getGunModel().transform.SetParent(mygun.handPos.root.GetChild(0));
                    mygun.SetupTransformGun.Invoke();
                    ikComponent.enabled = true;
                    Anim.SetBool("isCover", false);
                    Anim.SetBool("isReload", false);
                    Anim.SetBool(mygun.getAnimationClip(), true);
                    Anim.SetLayerWeight(1, 1);
                    break;

                case playerState.COVER:
                    mygun.getGunModel().transform.SetParent(mygun.handPos);
                    Anim.SetBool("isCover", true);
                    ikComponent.enabled = false;
                    Anim.SetLayerWeight(1, 0);
                    break;

                case playerState.DEATH:
                    Anim.enabled = false;
                    RagdollEvent.OnDeath(rigColliders, rigRigidbodies);
                    break;

                case playerState.RELOAD:
                    mygun.getGunModel().transform.SetParent(mygun.handPos);
                    mygun.getGunModel().transform.localPosition = Vector3.zero;
                    mygun.getGunModel().transform.localRotation = Quaternion.identity;                    
                    Anim.SetLayerWeight(2, 1);
                    Anim.SetBool("isCover", true);
                    Anim.SetBool("isReload", true);
                    ikComponent.enabled = false;
                    break;

                default:
                    break;
            }
        }
        public playerState GetState()
        {
            return myState;
        }
    }
}

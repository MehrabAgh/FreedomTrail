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

        private MainGun mygun;
        private Animator Anim;
        private Collider[] rigColliders;
        private Rigidbody[] rigRigidbodies;
        private FullBodyBipedIK ikComponent;

        public PlayerState(FullBodyBipedIK ikComp, MainGun gun, Animator anim, Collider[] RigColliders, Rigidbody[] RigRigidbodies)
        {
            rigColliders = RigColliders;
            rigRigidbodies = RigRigidbodies;
            mygun = gun;
            Anim = anim;
            ikComponent = ikComp;
            RagdollEvent.OnLive(rigColliders, rigRigidbodies);
        }


        #region helper
        private void ResetGunTransform()
        {
            mygun.getGunModel().transform.SetParent(mygun.handPos);
            mygun.getGunModel().transform.localPosition = Vector3.zero;
            mygun.getGunModel().transform.localRotation = Quaternion.identity;
        }
        public playerState GetState()
        {
            return myState;
        }
        #endregion
        public void EventStates(playerState state)
        {
            switch (state)
            {
                case playerState.IDLE:
                    ResetGunTransform();
                    ikComponent.enabled = false;
                    Anim.SetBool("isCover", false);
                    break;

                case playerState.ATTACK:
                    mygun.getGunModel().transform.SetParent(mygun.handPos);
                    mygun.SetupTransformGun.Invoke();
                    ikComponent.enabled = true;
                    //
                    Anim.GetComponent<CoverCharacter>().OnDisableCovers();
                    //
                    Anim.SetBool("isCover", false);
                    Anim.SetBool("isReload", false);
                    Anim.SetBool(mygun.getAnimationClip(), true);
                    Anim.SetLayerWeight(1, 1);
                    break;

                case playerState.COVER:
                    ResetGunTransform();
                    //
                    Anim.GetComponent<CoverCharacter>().OnEnableCovers();
                    //
                    Anim.SetBool("isCover", true);
                    ikComponent.enabled = false;
                    Anim.SetLayerWeight(1, 0);

                    break;

                case playerState.DEATH:
                    Anim.enabled = false;
                    ikComponent.enabled = false;
                    mygun.getGunModel().SetActive(false);
                    mygun.Ammos.Clear();
                    RagdollEvent.OnDeath(rigColliders, rigRigidbodies);
                    break;

                case playerState.RELOAD:
                    ResetGunTransform();
                    //
                    Anim.GetComponent<CoverCharacter>().OnEnableCovers();
                    //
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

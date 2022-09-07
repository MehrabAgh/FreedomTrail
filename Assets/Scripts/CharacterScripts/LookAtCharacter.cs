using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class LookAtCharacter : MonoBehaviour
    {
        [SerializeField] private Transform bone;
        [SerializeField] private Vector3 offset;
        public Transform target;
        private void LateUpdate()
        {
            LookAtTarget(target, bone);
        }
        public void LookAtTarget(Transform target, Transform bone)
        {
            bone.LookAt(target);
            bone.rotation = bone.rotation * Quaternion.Euler(offset);
        }
    }
}

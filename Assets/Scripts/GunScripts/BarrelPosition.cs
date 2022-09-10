using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs {
    public class BarrelPosition : MonoBehaviour
    {
        [SerializeField] private Transform barrelCurrent;
        private void Start()
        {
            barrelCurrent = GetComponentInParent<CharacterMain>().gunRes.barrel;
        }
        private void FixedUpdate()
        {
            transform.position = barrelCurrent.position;
        }
    }
}

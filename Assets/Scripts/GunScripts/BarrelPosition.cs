using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class BarrelPosition : MonoBehaviour
    {
        [SerializeField] private Transform barrelCurrent;
        private void Start()
        {
            barrelCurrent = GetComponentInParent<CharacterMain>().gunRes.barrel;
        }
        private void FixedUpdate()
        {
            if (barrelCurrent != null)
            {
                transform.position = barrelCurrent.position;
                transform.rotation =  barrelCurrent.rotation;
            }
            else
                barrelCurrent = GetComponentInParent<CharacterMain>().gunRes.barrel;
        }
    }
}

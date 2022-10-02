using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class BarrelPosition : MonoBehaviour
    {
        [SerializeField] private Transform barrelCurrent;      
        private void Update()
        {
            barrelCurrent = GetComponentInParent<CharacterMain>().gunRes.barrel;
            if (barrelCurrent != null)
            {
                transform.position = barrelCurrent.position;
                transform.rotation =  barrelCurrent.rotation;
            }           
        }
    }
}

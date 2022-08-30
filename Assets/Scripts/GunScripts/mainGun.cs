using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class mainGun : MonoBehaviour
    {             
        private WeaponResource myGun;
        [SerializeField] private List<WeaponScriptable> weaponSettings;
        private void Awake()
        {
            if (myGun == null)
            {
                PlayerPrefs.SetInt("Gun", 0);
                myGun = new WeaponResource(weaponSettings);                
            }
            myGun.WeaponSelected.Reload();
        }
        public void ShootGun()
        {
            myGun.WeaponSelected.Shoot();
        }

    }
}
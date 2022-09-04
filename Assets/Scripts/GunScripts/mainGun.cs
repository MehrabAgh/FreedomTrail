using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vino.Devs
{
    public class mainGun : MonoBehaviour
    {
        private WeaponResource myGun;
        [SerializeField] private List<WeaponScriptable> weaponSettings;
        [SerializeField] private Transform handPosition;
        [HideInInspector] public UnityEvent SetupTransformGun;
        [SerializeField] private List<ModelSizeGun> sizeGuns;

        public Transform handPos
        {
            get { return handPosition; }
            set { handPosition = value; }
        }

        private void Awake()
        {
            if (myGun == null)
            {
                PlayerPrefs.SetInt("Gun", 2);
                myGun = new WeaponResource(weaponSettings, handPosition, sizeGuns);
            }
            SetupTransformGun.AddListener(() => myGun.WeaponSelected.SaveTransform(myGun.GetSetting, sizeGuns[myGun.Index].Position,
                sizeGuns[myGun.Index].Rotation, sizeGuns[myGun.Index].Scale));
            ReloadGun();
        }

        public int ShootGun()
        {
            return myGun.WeaponSelected.Shoot();          
        }
        public string getAnimationClip()
        {
            return myGun.GetSetting.nameAnimate;
        }
        public GameObject getGunModel()
        {
            return myGun.WeaponSelected.weaponModel;
        }
        public void ReloadGun()
        {
            StartCoroutine(myGun.WeaponSelected.Reload(0.5f));
        }
    }
}
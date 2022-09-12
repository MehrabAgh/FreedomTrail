using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vino.Devs
{    
    public class MainGun : MonoBehaviour
    {
        public int CodeGun;
        //
        private WeaponResource myGun;
        [SerializeField] private List<WeaponScriptable> weaponSettings;
        [SerializeField] private List<GunResponse> weaponResponse;
        [SerializeField] private Transform handPosition;
        [HideInInspector] public UnityEvent SetupTransformGun;
        [SerializeField] private List<ModelSizeGun> sizeGuns;
        [HideInInspector]public List<GameObject> Ammos ;

        public Transform handPos
        {
            get { return handPosition; }
            set { handPosition = value; }
        }

        public void CreateGun()
        {
            myGun = new WeaponResource(weaponResponse, weaponSettings, handPosition, sizeGuns, Ammos, CodeGun);
            SetupTransformGun.AddListener(() => myGun.WeaponSelected.SaveTransform(myGun.GetSetting, sizeGuns[myGun.Index].Position,
                   sizeGuns[myGun.Index].Rotation, sizeGuns[myGun.Index].Scale));
            ReloadGun();
        }

        private void Awake()
        {
            Ammos = new List<GameObject>(0);
        }
        #region Helper     
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
        public GunResponse GetGunResponse()
        {
            return myGun.WeaponSelected.GetResponse();
        }
        #endregion
    }
}
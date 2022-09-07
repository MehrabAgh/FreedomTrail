using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vino.Devs
{    
    public class mainGun : MonoBehaviour
    {
        [HideInInspector] public int CodeGun;
        //
        private WeaponResource myGun;
        [SerializeField] private List<WeaponScriptable> weaponSettings;
        [SerializeField] private List<GunResponse> weaponResponse;
        [SerializeField] private Transform handPosition;
        [HideInInspector] public UnityEvent SetupTransformGun;
        [SerializeField] private List<ModelSizeGun> sizeGuns;
        public List<GameObject> Ammos ;

        public Transform handPos
        {
            get { return handPosition; }
            set { handPosition = value; }
        }

        private void Start()
        {
            if (myGun == null)
            {
                HandleSetGun(CodeGun);
                myGun = new WeaponResource(weaponResponse , weaponSettings, handPosition, sizeGuns, Ammos);
            }
            SetupTransformGun.AddListener(() => myGun.WeaponSelected.SaveTransform(myGun.GetSetting, sizeGuns[myGun.Index].Position,
                sizeGuns[myGun.Index].Rotation, sizeGuns[myGun.Index].Scale));
            ReloadGun();
        }


        #region Helper
        public int HandleGetGun()
        {
            return PlayerPrefs.GetInt("Gun");
        }
        private void HandleSetGun(int CodeGun)
        {
            PlayerPrefs.SetInt("Gun", CodeGun);
        }
        //
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
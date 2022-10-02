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
        public List<GameObject> Ammos ;

        public Transform HandPos
        {
            get { return handPosition; }
            set { handPosition = value; }
        }

        public void CreateGun()
        {          
            if (myGun != null)
                Destroy(myGun.WeaponSelected.weaponModel);
            myGun = new WeaponResource(weaponResponse, weaponSettings, handPosition, sizeGuns, Ammos, CodeGun);         
            SetupTransformGun.AddListener(() => myGun.WeaponSelected.SaveTransform(myGun.GetSetting, sizeGuns[myGun.Index].Position,
                   sizeGuns[myGun.Index].Rotation, sizeGuns[myGun.Index].Scale));            
        }

        private void Awake() => Ammos = new List<GameObject>(0);
        #region Helper     
        public void ShootGun()
        {            
            myGun.WeaponSelected.Shoot();          
        }
        public string GetAnimationClip()
        {
            return myGun.GetSetting.nameAnimate;
        }
        public GameObject GetGunModel()
        {
            return myGun.WeaponSelected.weaponModel;
        }
        public GunResponse GetGunResponse()
        {
            return myGun.WeaponSelected.GetResponse();
        }
        #endregion
    }
}
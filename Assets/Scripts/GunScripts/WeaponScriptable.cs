using System;
using UnityEngine;
using Vino.Devs;

namespace Vino.Devs
{
    [Serializable]
    public struct ModelSizeGun
    {
        public Vector3 Position, Rotation, Scale;
    }

    [CreateAssetMenu(fileName = "Gun", menuName = "GunSystems/newGun", order = 1)]
    public class WeaponScriptable : ScriptableObject
    {
        public GameObject weaponeModels;
        public WeaponResource.weapone getWeapone;
        public Transform barrel;
        public GameObject bullet;
        public float delay;
        public float nextTimetoFire;
        public float power = 200;
        public int maxReload , currentAmmo;
        [System.NonSerialized]
        public float burstshotCount;

        public string nameAnimate;
    
        public void DefaultShoot()
        {
            var projectile = AmmoPooling.instanse.GetPooledObject();
            projectile.transform.position = barrel.transform.position;
            projectile.transform.localRotation = barrel.transform.localRotation ;
            projectile.SetActive(true);           
            projectile.GetComponent<Rigidbody>().velocity = barrel.up * power;
        }

        public void SetTransform(Vector3 pos, Vector3 rot, Vector3 scale, Transform model)
        {
            model.transform.localPosition = pos;
            model.transform.localRotation = Quaternion.Euler(rot);
            model.transform.localScale = scale;
        }  
    }
}
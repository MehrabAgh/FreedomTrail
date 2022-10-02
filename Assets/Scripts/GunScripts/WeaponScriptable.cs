using System;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

namespace Vino.Devs
{
    [Serializable]
    public struct ModelSizeGun
    {
        public Vector3 Position, Rotation, Scale;
    }
    [Serializable]
    public class GunResponse
    {
        public Transform barrel , mainBarrel;
        public GameObject bullet;
        public int maxReload, currentAmmo;
        public float delay;
        public float nextTimetoFire;
    }
    [CreateAssetMenu(fileName = "Gun", menuName = "GunSystems/newGun", order = 1)]
    public class WeaponScriptable : ScriptableObject
    {
        public GameObject weaponeModels;
        public WeaponResource.weapone getWeapone;                      
        public float power = 200;
    
        [System.NonSerialized]
        public float burstshotCount;

        public string nameAnimate;
    
        public void DefaultShoot(Transform barrel,  List<GameObject> ammos)
        {         
            var projectile = AmmoPooling.instanse.GetPooledObject(ammos);
            projectile.transform.position = barrel.transform.position;  
            projectile.transform.rotation = barrel.transform.rotation;
            projectile.SetActive(true);           
            projectile.GetComponent<Rigidbody>().velocity = barrel.forward * power;
        }

        public void SetTransform(Vector3 pos, Vector3 rot, Vector3 scale, Transform model)
        {
            model.transform.localPosition = pos;
            model.transform.localRotation = Quaternion.Euler(rot);
            model.transform.localScale = scale;
        }  
    }
}
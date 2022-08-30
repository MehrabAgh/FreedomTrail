using UnityEngine;
using Vino.Devs;

namespace Vino.Devs
{
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

        [System.NonSerialized]
        public float burstshotCount;
        public void DefaultShoot()
        {
            GameObject projectile = Instantiate(bullet, barrel.position , barrel.rotation);
            projectile.GetComponent<Rigidbody>().velocity = barrel.up * power;
        }
    }
}
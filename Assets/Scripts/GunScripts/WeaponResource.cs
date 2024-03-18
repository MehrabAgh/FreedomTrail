using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{

    public class WeaponResource : MonoBehaviour
    {
        public enum weapone { rifle, shotgun, machinegun, minigun , pistol}; // 2 : add name new gun to state guns
        // 3: add new gun setting to list in Main Gun Class
        private weapone curWeapone;
        public IGun WeaponSelected;
        public int Index;
        public WeaponScriptable GetSetting;
        public GunResponse GetResponse;        

        public void Initialize(List<GunResponse> gunResponses, List<WeaponScriptable> weaponSettings,
            Transform piv, List<ModelSizeGun> sizeList, List<GameObject> Ammos, int IndexWeapone)
        {
            curWeapone = (weapone)IndexWeapone;
            switch (curWeapone) // 4: add state new gun to this conditional
            {
                case weapone.rifle:
                    Index = 0;
                    Rifle rifle = gameObject.AddComponent<Rifle>();
                    rifle.Setup(gunResponses[0], weaponSettings[0], piv, sizeList[0].Position, sizeList[0].Rotation, sizeList[0].Scale, Ammos);
                    WeaponSelected = rifle;
                    GetSetting = rifle.GetSetting();
                    GetResponse = rifle.GetResponse();
                    break;
                case weapone.shotgun:
                    Index = 1;
                    Shotgun shotgun = gameObject.AddComponent<Shotgun>();
                    shotgun.Setup(gunResponses[1], weaponSettings[1], piv, sizeList[1].Position, sizeList[1].Rotation, sizeList[1].Scale, Ammos);
                    GetSetting = shotgun.GetSetting();
                    GetResponse = shotgun.GetResponse();
                    WeaponSelected = shotgun;
                    break;
                case weapone.machinegun:
                    Index = 2;
                    LMG lmg = gameObject.AddComponent<LMG>();
                    lmg.Setup(gunResponses[2], weaponSettings[2], piv, sizeList[2].Position, sizeList[2].Rotation, sizeList[2].Scale, Ammos);
                    GetSetting = lmg.GetSetting();
                    GetResponse = lmg.GetResponse();
                    WeaponSelected = lmg;
                    break;
                case weapone.minigun:
                    Index = 3;
                    SMG smg = gameObject.AddComponent<SMG>();
                    smg.Setup(gunResponses[3], weaponSettings[3], piv, sizeList[3].Position, sizeList[3].Rotation, sizeList[3].Scale, Ammos);
                    GetSetting = smg.GetSetting();
                    GetResponse = smg.GetResponse();
                    WeaponSelected = smg;
                    break;
                case weapone.pistol:
                    Index = 4;
                    Pistol pis = gameObject.AddComponent<Pistol>();
                    pis.Setup(gunResponses[4], weaponSettings[4], piv, sizeList[4].Position, sizeList[4].Rotation, sizeList[4].Scale, Ammos);
                    GetSetting = pis.GetSetting();
                    GetResponse = pis.GetResponse();
                    WeaponSelected = pis;
                    break;
                default:
                    break;
            }
        }
    }
    // 1 : add class and attribute new gun
    public class SMG : MainGun, IGun
    {
        private WeaponScriptable scriptable;
        private GunResponse gunRes;
        private new List<GameObject> Ammos;
        public GameObject weaponModel { get; set; }     
        public void Setup(GunResponse gn, WeaponScriptable wr, Transform handppivot, Vector3 pos, Vector3 rot, Vector3 scale, List<GameObject> ammos)
        {
            Ammos = ammos;
            gunRes = gn;
            scriptable = wr;
            weaponModel = Instantiate(wr.weaponeModels, handppivot.position, handppivot.rotation, handppivot);
            SaveTransform(wr, pos, rot, scale);
            gn.barrel = weaponModel.transform.GetChild(0);
            gn.ShootFX = ParticleManager.instanse.ShootGun;
        }
        public WeaponScriptable GetSetting()
        {
            return scriptable;
        }
        public GunResponse GetResponse()
        {
            return gunRes;
        }
        public IEnumerator Reload(float time)
        {
            if (gunRes.currentAmmo < 1)
            {
                gunRes.currentAmmo = gunRes.maxReload;
            }
            yield return new WaitForSeconds(time);
        }
        public void Shoot()
        {
            gunRes.nextTimetoFire -= Time.deltaTime;
            if (gunRes.nextTimetoFire <= 0)
            {
                gunRes.nextTimetoFire = gunRes.delay;
                scriptable.DefaultShoot(gunRes.mainBarrel, Ammos);
                ParticleManager.instanse.Spawn(gunRes.ShootFX, gunRes.mainBarrel, .7f);
                gunRes.currentAmmo -= 1;
            }
        }
        public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            wr.SetTransform(pos, rot, scale, weaponModel.transform);
        }
    }


    public class LMG : MainGun, IGun
    {
        private GunResponse gunRes;
        private WeaponScriptable scriptable;
        private new List<GameObject> Ammos;
        public GameObject weaponModel { get; set; }
        public void Setup(GunResponse gn, WeaponScriptable wr, Transform handppivot, Vector3 pos, Vector3 rot, Vector3 scale, List<GameObject> ammos)
        {
            Ammos = ammos;
            gunRes = gn;
            scriptable = wr;
            weaponModel = Instantiate(wr.weaponeModels, handppivot.position, handppivot.rotation, handppivot);
            gn.barrel = weaponModel.transform.GetChild(0);
            SaveTransform(wr, pos, rot, scale);
            gn.ShootFX = ParticleManager.instanse.ShootGun;
        }
        public WeaponScriptable GetSetting()
        {
            return scriptable;
        }
        public GunResponse GetResponse()
        {
            return gunRes;
        }
        public IEnumerator Reload(float time)
        {
            if (gunRes.currentAmmo < 1)
            {
                gunRes.currentAmmo = gunRes.maxReload;
            }
            yield return new WaitForSeconds(time);

        }
        public void Shoot()
        {
            gunRes.nextTimetoFire -= Time.deltaTime;
            if (gunRes.nextTimetoFire <= 0)
            {
                gunRes.nextTimetoFire = gunRes.delay;
                scriptable.DefaultShoot(gunRes.mainBarrel, Ammos);
                ParticleManager.instanse.Spawn(gunRes.ShootFX, gunRes.mainBarrel, .7f);
                gunRes.currentAmmo -= 1;
            }
        }
        public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            wr.SetTransform(pos, rot, scale, weaponModel.transform);
        }        
    }


    public class Rifle : MainGun, IGun
    {
        private GunResponse gunRes;
        private WeaponScriptable scriptable;
        private new List<GameObject> Ammos;
        public GameObject weaponModel { get; set; }
        public void Setup(GunResponse gn, WeaponScriptable wr, Transform handppivot, Vector3 pos, Vector3 rot, Vector3 scale, List<GameObject> ammos)
        {
            Ammos = ammos;
            gunRes = gn;
            scriptable = wr;
            weaponModel = Instantiate(wr.weaponeModels, handppivot.position, handppivot.rotation, handppivot.transform.root.GetChild(0));
            gunRes.barrel = weaponModel.transform.GetChild(0);
            SaveTransform(wr, pos, rot, scale);
            gn.ShootFX = ParticleManager.instanse.ShootGun;
        }
        public WeaponScriptable GetSetting()
        {
            return scriptable;
        }
        public GunResponse GetResponse()
        {
            return gunRes;
        }
        public IEnumerator Reload(float time)
        {

            if (gunRes.currentAmmo < 1)
            {
                gunRes.currentAmmo = gunRes.maxReload;
            }
            yield return new WaitForSeconds(time);
        }
        public void Shoot()
        {
            gunRes.nextTimetoFire -= Time.deltaTime;
            if (gunRes.nextTimetoFire <= 0)
            {
                if (scriptable.burstshotCount == 3)
                {
                    gunRes.nextTimetoFire = gunRes.delay;
                    scriptable.burstshotCount = 0;
                }
                else
                {
                    gunRes.nextTimetoFire = 0.1f;
                }
                scriptable.DefaultShoot(gunRes.mainBarrel, Ammos);
                ParticleManager.instanse.Spawn(gunRes.ShootFX, gunRes.mainBarrel, .7f);
                gunRes.currentAmmo -= 1;
                scriptable.burstshotCount++;
            }
        }
        public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            wr.SetTransform(pos, rot, scale, weaponModel.transform);
        }
    }


    public class Shotgun : MainGun, IGun
    {
        private WeaponScriptable scriptable;
        private GunResponse gunRes;
        public GameObject weaponModel { get; set; }
        private new List<GameObject> Ammos;   
        public void Setup(GunResponse gn, WeaponScriptable wr, Transform handppivot, Vector3 pos, Vector3 rot, Vector3 scale, List<GameObject> ammos)
        {
            Ammos = ammos;
            gunRes = gn;
            scriptable = wr;
            weaponModel = Instantiate(wr.weaponeModels, handppivot.position, handppivot.rotation, handppivot);
            gn.barrel = weaponModel.transform.GetChild(0);
            SaveTransform(wr, pos, rot, scale);
            gn.ShootFX = ParticleManager.instanse.ShootGun;
        }
        public WeaponScriptable GetSetting()
        {
            return scriptable;
        }
        public GunResponse GetResponse()
        {
            return gunRes;
        }
        public IEnumerator Reload(float time)
        {

            if (gunRes.currentAmmo < 1)
            {
                gunRes.currentAmmo = gunRes.maxReload;
            }
            yield return new WaitForSeconds(time);

        }
        public void Shoot()
        {
            gunRes.nextTimetoFire -= Time.deltaTime;
            if (gunRes.nextTimetoFire <= 0)
            {
                gunRes.nextTimetoFire = gunRes.delay;
                for (int i = 0; i < 8; i++)
                {
                    Vector3 direction = gunRes.mainBarrel.forward;
                    Vector3 spread = Vector3.zero;
                    spread += gunRes.mainBarrel.up * Random.Range(-1, 1);
                    spread += gunRes.mainBarrel.right * Random.Range(-1, 1);
                    direction += spread.normalized * Random.Range(0, 0.15f);
                    var projectile = AmmoPooling.instanse.GetPooledObject(Ammos);
                    projectile.transform.position = gunRes.mainBarrel.transform.position;
                    projectile.transform.rotation = gunRes.mainBarrel.transform.rotation;
                    projectile.SetActive(true);
                    projectile.GetComponent<Rigidbody>().velocity = direction * scriptable.power;
                }
                ParticleManager.instanse.Spawn(gunRes.ShootFX, gunRes.mainBarrel, .7f);
                gunRes.currentAmmo -= 1;
            }
        }
        public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            wr.SetTransform(pos, rot, scale, weaponModel.transform);
        }
    }


    public class Pistol : MainGun, IGun
    {
        private WeaponScriptable scriptable;
        private GunResponse gunRes;
        private new List<GameObject> Ammos;
        public GameObject weaponModel { get; set ; }        

        public void Setup(GunResponse gn, WeaponScriptable wr, Transform handppivot, Vector3 pos, Vector3 rot, Vector3 scale, List<GameObject> ammos)
        {
            Ammos = ammos;
            gunRes = gn;
            scriptable = wr;
            weaponModel = Instantiate(wr.weaponeModels, handppivot.position, handppivot.rotation, handppivot);
            SaveTransform(wr, pos, rot, scale);
            gn.barrel = weaponModel.transform.GetChild(0);
            gunRes.ShootFX = ParticleManager.instanse.ShootGun;
        }

        public GunResponse GetResponse()
        {
            return gunRes;
        }

        public WeaponScriptable GetSetting()
        {
            return scriptable;
        }

        public IEnumerator Reload(float time)
        {
            if (gunRes.currentAmmo < 1)
            {
                gunRes.currentAmmo = gunRes.maxReload;
            }
            yield return new WaitForSeconds(time);
        }

        public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            wr.SetTransform(pos, rot, scale, weaponModel.transform);
        }

        public void Shoot()
        {
            gunRes.nextTimetoFire -= Time.deltaTime;
            if (gunRes.nextTimetoFire <= 0)
            {
                gunRes.nextTimetoFire = gunRes.delay;
                scriptable.DefaultShoot(gunRes.mainBarrel, Ammos);                                
                ParticleManager.instanse.Spawn(gunRes.ShootFX, gunRes.mainBarrel, 2f);                
                gunRes.currentAmmo -= 1;
            }
        }
    }
}
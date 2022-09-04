using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vino.Devs
{
    public class WeaponResource
    {
        public enum weapone { rifle, shotgun, machinegun, minigun }; // 2 : add name new gun to state guns
        // 3: add new gun setting to list in Main Gun Class
        private weapone curWeapone;
        public IGun WeaponSelected;
        public int Index;
        public WeaponScriptable GetSetting;
        public WeaponResource(List<WeaponScriptable> weaponSettings, Transform piv, List<ModelSizeGun> sizeList)
        {
            curWeapone = (weapone)PlayerPrefs.GetInt("Gun");
            SettingWeapone(curWeapone, weaponSettings, piv, sizeList);
        }
        private void SettingWeapone(weapone scriptable, List<WeaponScriptable> weaponSettings, Transform piv, List<ModelSizeGun> sizeList)
        {
            switch (scriptable) // 4: add state new gun to this conditional
            {
                case weapone.rifle:
                    Index = 0;
                    Rifle rifle = new Rifle(weaponSettings[0], piv, sizeList[0].Position, sizeList[0].Rotation, sizeList[0].Scale);
                    WeaponSelected = rifle;
                    GetSetting = rifle.GetSetting();
                    break;
                case weapone.shotgun:
                    Index = 1;
                    IGun shotgun = new Shotgun(weaponSettings[1], piv, sizeList[1].Position, sizeList[1].Rotation, sizeList[1].Scale);
                    GetSetting = shotgun.GetSetting();
                    WeaponSelected = shotgun;
                    break;
                case weapone.machinegun:
                    Index = 2;
                    LMG lmg = new LMG(weaponSettings[2], piv, sizeList[2].Position, sizeList[2].Rotation, sizeList[2].Scale);
                    GetSetting = lmg.GetSetting();
                    WeaponSelected = lmg;
                    break;
                case weapone.minigun:
                    Index = 3;
                    SMG smg = new SMG(weaponSettings[3], piv, sizeList[3].Position, sizeList[3].Rotation, sizeList[3].Scale);
                    GetSetting = smg.GetSetting();
                    WeaponSelected = smg;
                    break;
                default:
                    break;
            }
        }
    }
    // 1 : add class and attribute new gun
    public class SMG : mainGun, IGun
    {
        private WeaponScriptable scriptable;
        public GameObject weaponModel { get; set; }
        public SMG(WeaponScriptable wr, Transform handppivot, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            scriptable = wr;
            weaponModel = Instantiate(wr.weaponeModels, handppivot.position, handppivot.rotation, handppivot);
            SaveTransform(wr, pos, rot, scale);
            wr.barrel = weaponModel.transform.GetChild(0);
        }
        public WeaponScriptable GetSetting()
        {
            return scriptable;
        }
        public IEnumerator Reload(float time)
        {
            if (scriptable.currentAmmo < 1)
            {
                scriptable.currentAmmo = scriptable.maxReload;
            }
            yield return new WaitForSeconds(time);
        }
        public int Shoot()
        {
            if (scriptable.currentAmmo > 0)
            {
                scriptable.currentAmmo -= 1;
                scriptable.nextTimetoFire -= Time.deltaTime;
                if (scriptable.nextTimetoFire <= 0)
                {
                    scriptable.nextTimetoFire = scriptable.delay;
                    scriptable.DefaultShoot();
                    scriptable.currentAmmo -= 1;
                }
                return 1;
            }
            else
            {
                return -1;
            }
        }


        public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            wr.SetTransform(pos, rot, scale, weaponModel.transform);
        }
    }


    public class LMG : mainGun, IGun
    {
        private WeaponScriptable scriptable;
        public GameObject weaponModel { get; set; }
        public LMG(WeaponScriptable wr, Transform handppivot, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            scriptable = wr;
            weaponModel = Instantiate(wr.weaponeModels, handppivot.position, handppivot.rotation, handppivot);
            wr.barrel = weaponModel.transform.GetChild(0);
            SaveTransform(wr, pos, rot, scale);
        }

        public WeaponScriptable GetSetting()
        {
            return scriptable;
        }

        public IEnumerator Reload(float time)
        {
            if (scriptable.currentAmmo < 1)
            {
                scriptable.currentAmmo = scriptable.maxReload;
            }
            yield return new WaitForSeconds(time);

        }

        public int Shoot()
        {
            if (scriptable.currentAmmo > 0)
            {
                scriptable.nextTimetoFire -= Time.deltaTime;
                if (scriptable.nextTimetoFire <= 0)
                {
                    scriptable.nextTimetoFire = scriptable.delay;
                    scriptable.DefaultShoot();
                    scriptable.currentAmmo -= 1;
                }
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            wr.SetTransform(pos, rot, scale, weaponModel.transform);
        }
    }


    public class Rifle : mainGun, IGun
    {
        private WeaponScriptable scriptable;
        public GameObject weaponModel { get; set; }

        public Rifle(WeaponScriptable wr, Transform handppivot, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            scriptable = wr;
            weaponModel = Instantiate(wr.weaponeModels, handppivot.position, handppivot.rotation, handppivot.transform.root.GetChild(0));
            wr.barrel = weaponModel.transform.GetChild(0);
            SaveTransform(wr, pos, rot, scale);
        }
        public WeaponScriptable GetSetting()
        {
            return scriptable;
        }
        public IEnumerator Reload(float time)
        {

            if (scriptable.currentAmmo < 1)
            {
                scriptable.currentAmmo = scriptable.maxReload;
            }
            yield return new WaitForSeconds(time);
        }
        public int Shoot()
        {
            if (scriptable.currentAmmo > 0)
            {
                scriptable.nextTimetoFire -= Time.deltaTime;
                if (scriptable.nextTimetoFire <= 0)
                {
                    if (scriptable.burstshotCount == 3)
                    {
                        scriptable.nextTimetoFire = scriptable.delay;
                        scriptable.burstshotCount = 0;
                    }
                    else
                    {
                        scriptable.nextTimetoFire = 0.1f;
                    }
                    scriptable.DefaultShoot();
                    scriptable.currentAmmo -= 1;
                    scriptable.burstshotCount++;
                }
                return 1;
            }
            else
            {
                return -1;
            }
        }
        public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            wr.SetTransform(pos, rot, scale, weaponModel.transform);
        }
    }


    public class Shotgun : mainGun, IGun
    {
        private WeaponScriptable scriptable;
        public GameObject weaponModel { get; set; }
        public Shotgun(WeaponScriptable wr, Transform handppivot, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            scriptable = wr;
            weaponModel = Instantiate(wr.weaponeModels, handppivot.position, handppivot.rotation, handppivot);
            wr.barrel = weaponModel.transform.GetChild(0);
            SaveTransform(wr, pos, rot, scale);
        }
        public WeaponScriptable GetSetting()
        {
            return scriptable;
        }
        public IEnumerator Reload(float time)
        {

            if (scriptable.currentAmmo < 1)
            {
                scriptable.currentAmmo = scriptable.maxReload;
            }
            yield return new WaitForSeconds(time);

        }
        public int Shoot()
        {
            if (scriptable.currentAmmo > 0)
            {
                scriptable.nextTimetoFire -= Time.deltaTime;
                if (scriptable.nextTimetoFire <= 0)
                {
                    scriptable.nextTimetoFire = scriptable.delay;
                    for (int i = 0; i < 8; i++)
                    {
                        Vector3 direction = scriptable.barrel.forward;
                        Vector3 spread = Vector3.zero;
                        spread += scriptable.barrel.up * Random.Range(-1, 1);
                        spread += scriptable.barrel.right * Random.Range(-1, 1);
                        direction += spread.normalized * Random.Range(0, 0.15f);
                        var projectile = AmmoPooling.instanse.GetPooledObject();
                        projectile.transform.position = scriptable.barrel.transform.position;
                        // projectile.transform.rotation = scriptable.barrel.transform.rotation;
                        projectile.SetActive(true);
                        //GameObject projectile = Instantiate(scriptable.bullet, scriptable.barrel.position + (scriptable.barrel.forward * 2),
                        //Quaternion.LookRotation(Vector3.RotateTowards(scriptable.barrel.forward, direction, 1, 0))
                        //);
                        projectile.GetComponent<Rigidbody>().velocity = direction * scriptable.power;
                    }
                }
                return 1;
            }
            else
            {
                return -1;
            }
        }
        public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
        {
            wr.SetTransform(pos, rot, scale, weaponModel.transform);
        }
    }

    //public class Pistol : mainGun, IGun
    //{
    //    public GameObject weaponModel { get ; set ; }

    //    public WeaponScriptable GetSetting()
    //    {
    //        return;
    //    }

    //    public IEnumerator Reload(float time)
    //    {         
    //        yield
    //    }

    //    public void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale)
    //    {           
    //    }

    //    public int Shoot()
    //    {          
    //    }
    //}
}
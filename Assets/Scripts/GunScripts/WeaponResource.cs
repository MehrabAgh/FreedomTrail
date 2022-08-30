using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class WeaponResource
    {
        public enum weapone { rifle, shotgun, machinegun, minigun }; // 2 : add name new gun to state guns
        // 3: add new gun setting to list in Main Gun Class
        private weapone curWeapone;
        public IGun WeaponSelected;

        public WeaponResource(List<WeaponScriptable> weaponSettings)
        {
            curWeapone = (weapone)PlayerPrefs.GetInt("Gun");
            SettingWeapone(curWeapone, weaponSettings);
        }
        private void SettingWeapone(weapone scriptable ,  List<WeaponScriptable> weaponSettings)
        {
            switch (scriptable) // 4: add state new gun to this conditional
            {
                case weapone.rifle:
                    Rifle rifle = new Rifle(weaponSettings[0]);                                    
                    WeaponSelected = rifle;
                    break;
                case weapone.shotgun:
                    IGun shotgun = new Shotgun(weaponSettings[1]);                    
                    WeaponSelected = shotgun;                                      
                    break;
                case weapone.machinegun:
                    LMG lmg = new LMG(weaponSettings[2]);                    
                    WeaponSelected = lmg;
                    break;
                case weapone.minigun:
                    SMG smg = new SMG(weaponSettings[3]);                    
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
        public SMG(WeaponScriptable wr)
        {
            scriptable = wr;
            Instantiate(wr.weaponeModels);
        }

        public void Reload()
        {
            print("SMG");
        }

        public void Shoot()
        {
            if (scriptable.nextTimetoFire <= 0)
            {
                scriptable.nextTimetoFire = scriptable.delay;
                scriptable.DefaultShoot();
            }
        }
    }
    public class LMG : mainGun, IGun
    {
        private WeaponScriptable scriptable;
        public LMG(WeaponScriptable wr)
        {
            scriptable = wr;
            Instantiate(wr.weaponeModels);
        }

        public void Reload()
        {
            print("LMG");
        }

        public void Shoot()
        {
            if (scriptable.nextTimetoFire <= 0)
            {
                scriptable.nextTimetoFire = scriptable.delay;
                scriptable.DefaultShoot();
            }
        }
    }
    public class Rifle : mainGun, IGun
    {
        private WeaponScriptable scriptable;
        public Rifle(WeaponScriptable wr)
        {
            scriptable = wr;
            var x = Instantiate(wr.weaponeModels);
            wr.barrel = x.transform.GetChild(0);
        }

        public void Reload()
        {
            print("Rifle");
        }

        public void Shoot()
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
                scriptable.burstshotCount++;
            }
        }
    }
    public class Shotgun : mainGun, IGun
    {
        private WeaponScriptable scriptable;
        public Shotgun(WeaponScriptable wr)
        {
            scriptable = wr;
            Instantiate(wr.weaponeModels);
        }

        public void Reload()
        {
            print("Shotgun");
        }

        public void Shoot()
        {
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

                    GameObject projectile = Instantiate(scriptable.bullet, scriptable.barrel.position + (scriptable.barrel.forward * 2),
                    Quaternion.LookRotation(Vector3.RotateTowards(scriptable.barrel.forward, direction, 1, 0))
                    );
                    projectile.GetComponent<Rigidbody>().velocity = direction * scriptable.power;
                }
            }
        }
    }
}
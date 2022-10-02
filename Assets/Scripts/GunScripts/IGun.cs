using System.Collections;
using UnityEngine;
using Vino.Devs;

public interface IGun
{   
    public GameObject weaponModel { get; set; }
    public abstract void Shoot();
    public abstract IEnumerator Reload(float time);
    public abstract WeaponScriptable GetSetting();
    public abstract GunResponse GetResponse();
    public abstract void SaveTransform(WeaponScriptable wr, Vector3 pos, Vector3 rot, Vector3 scale);
}
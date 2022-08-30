using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GunFuncManage : MonoBehaviour
{
    public float Power;
    public int MaxAmmo, AmmoCount;
    public GameObject Ammo;
    public float TimeReload;
    public Transform[] pivot;
    public abstract void Shoot();
    public abstract void Reload();
    public abstract void FindPivot();
}

public class GunManager : GunFuncManage
{
   // public PlayerMovement player;
    private GameObject SubAmmo;
    private float SubtimeReload;
    //
    public override void Reload()
    {
        if (TimeReload > 0)
        {
            TimeReload -= Time.deltaTime;
        }
        else if (TimeReload <= 0)
        {
            AmmoCount = MaxAmmo;
            TimeReload = SubtimeReload;
        }   
    }
    public override void FindPivot()
    {
        pivot = transform.GetComponentsInChildren<Transform>();
        pivot = pivot.Where(child => child.name == "pivot").ToArray();
    }
    public override void Shoot()
    {
        for (int i = 0; i < pivot.Length; i++)
        {
            SubAmmo = Instantiate(Ammo, pivot[i].position, pivot[i].rotation);
            SubAmmo.GetComponent<Rigidbody>().AddForce(SubAmmo.transform.forward * Power);
            AmmoCount--;
            //player.GetComponent<Animator>().SetLayerWeight(1, 1);
        }
       
    }
    //  
    private void Start()
    {
        FindPivot();
        AmmoCount = MaxAmmo;
        SubtimeReload = TimeReload;
    }
    public void Shooted()
    {
        if (AmmoCount > 0)
        {
            Shoot();
           
        }
        else if (AmmoCount <= 0)
        {
            Reload();
          
        }
    }
}

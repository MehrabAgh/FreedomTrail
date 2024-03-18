using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem   CharHitFx, ShootGun , ObjHitFx;    

    public static ParticleManager instanse;    

    private void Awake()
    {
        instanse = this;
    }

    public void EnableEffect(ParticleSystem ps)
    {        
        ps.gameObject.SetActive(true);        
    }

    public void DisableEffect(ParticleSystem ps)
    {
        ps.gameObject.SetActive(false);
    }

    public void Spawn(ParticleSystem ps , Transform t , float delay)
    {        
        Destroy(Instantiate(ps, t.transform.position, t.transform.rotation), delay);
    }
}

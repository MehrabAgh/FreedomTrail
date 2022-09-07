using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

[RequireComponent(typeof(HealthCharacter))]
[RequireComponent(typeof(LookAtCharacter))]
public class MainPlayer : PlayerMovement
{
    private PlayerState mystate;
    private HealthCharacter myhealth;
    [HideInInspector]public GunResponse gunRes;
    private Animator anim;
    private Collider[] rigColliders;
    private Rigidbody[] rigRigidbodies;
    public int GunCode = 0;
    private FullBodyBipedIK ikComponent;
    [HideInInspector]public mainGun gun;
    [HideInInspector]public CoverCharacter myCover;
    private void Awake()
    {       
        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        gun = GetComponent<mainGun>();       
        
        anim = GetComponent<Animator>();
        myCover = GetComponent<CoverCharacter>();
        ikComponent = GetComponent<FullBodyBipedIK>();
        myhealth = GetComponent<HealthCharacter>();
        gun.CodeGun = GunCode;        
    }    
    private void Start()
    {
        gunRes = gun.GetGunResponse();
        mystate = new PlayerState(ikComponent, gun, anim, rigColliders, rigRigidbodies);     
        GetComponent<Collider>().enabled = true;
        AmmoPooling.instanse.objectToPool = gunRes.bullet;
        AmmoPooling.instanse.Spawning(transform.parent, gun.Ammos);        
        StartCoroutine(update(0.003f));      
    }

    #region Helper
    public void SetReload()
    {
        PlayerState.myState = PlayerState.playerState.RELOAD;
        mystate.EventStates(PlayerState.myState);
    }    
    #endregion   
    private IEnumerator update(float timer)
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);

            if (myhealth.getHealth() < 1)
            {
                PlayerState.myState = PlayerState.playerState.DEATH;
                mystate.EventStates(PlayerState.myState);
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) StartMove();

                if (Input.GetMouseButton(0))
                {
                    if (gun.ShootGun() > -1)
                    {
                        PlayerState.myState = PlayerState.playerState.ATTACK;
                        mystate.EventStates(PlayerState.myState);
                    }
                    else
                    {
                        SetReload();
                    }
                    UpdateMove();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    PlayerState.myState = PlayerState.playerState.COVER;
                    mystate.EventStates(PlayerState.myState);
                    EndMove();
                }
            }

        }
    }
}

using RootMotion.FinalIK;
using System;
using System.Collections;
using UnityEngine;
using Vino.Devs;

public class MainPlayer : PlayerMovement
{
    private PlayerState mystate;
    private HealthCharacter myhealth;   
    private Animator anim;
    private Collider[] rigColliders;
    private Rigidbody[] rigRigidbodies;
    private FullBodyBipedIK ikComponent;

    public mainGun gun;
    public static MainPlayer instance;
    private void Awake()
    {
        instance = this;
        rigColliders = GetComponentsInChildren<Collider>();
        rigRigidbodies = GetComponentsInChildren<Rigidbody>();
        gun = GetComponent<mainGun>();
        anim = GetComponent<Animator>();
        ikComponent = GetComponent<FullBodyBipedIK>();
        myhealth = GetComponent<HealthCharacter>();
    }    
    private void Start()
    {
        mystate = new PlayerState(ikComponent, gun, anim, rigColliders, rigRigidbodies);     
        GetComponent<Collider>().enabled = true;        
        StartCoroutine(update(0.003f));      
    }

    public void SetReload()
    {
        PlayerState.myState = PlayerState.playerState.RELOAD;
        mystate.EventStates(PlayerState.myState);
    }

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

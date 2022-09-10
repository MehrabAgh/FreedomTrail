using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(HealthCharacter))]
[RequireComponent(typeof(LookAtCharacter))]
public class MainPlayer : CharacterMain
{
    private PlayerMovement mymovement;
    private PlayerState mystate;       
    [HideInInspector]public CoverCharacter myCover; 
    private void Start()
    {
        mymovement = GetComponent<PlayerMovement>();
        myCover = GetComponent<CoverCharacter>();              
        mystate = new PlayerState(ikComponent, gun, Anim, rigColliders, rigRigidbodies);       
        SetIdle();
        GetComponent<Collider>().enabled = true;
        AmmoPooling.instanse.objectToPool = gunRes.bullet;        
        AmmoPooling.instanse.Spawning(parentAmmo, gun.Ammos);        
        StartCoroutine(update(0.001f));        
    }

    #region Helper
    public void SetReload()
    {
        PlayerState.myState = PlayerState.playerState.RELOAD;
        mystate.EventStates(PlayerState.myState);
    }    
    public void SetIdle()
    {      
        PlayerState.myState = PlayerState.playerState.IDLE;
        mystate.EventStates(PlayerState.myState);
        GetComponent<Rigidbody>().isKinematic = false;
    }
    public bool CheckColliderGround()
    {
        if (launchingProjectiles.ins.DistanceTarget() <= 0.1f)
        { 
            GameManager.instance._isStartGame = false;
            launchingProjectiles.ins.ThisObject.SetParent(launchingProjectiles.ins.TargetObject.parent);
            transform.SetSiblingIndex(0);           
            return false;
        }
        else return true;
    }
    public float CheckDistancetoEnd()
    {
        return Vector3.Distance(transform.position, GameManager.instance.EndPos.position);
    }
    private void StartExiting()
    {
        GameManager.instance.HeliCopter.GetComponent<AnimAIHelicopter>()
          .animState = AnimAIHelicopter.HeliAnimState.StartExiting;
    }
    #endregion

    #region Start And End Cinematic Events
    public void StartGame()
    {
        Anim.SetBool("StartCinematic", true);
        SetIdle();      
        launchingProjectiles.ins.Launch();       
        Invoke("StartExiting", 2f);
        //play Animation Start Cinematic
    }
    #endregion


    private IEnumerator update(float timer)
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            CheckColliderGround();
            if (!GameManager.instance._isStartGame && !GameManager.instance._isEndGame)
            {
                Anim.SetBool("StartCinematic", CheckColliderGround());
                if (myhealth.getHealth() < 1)
                {
                    PlayerState.myState = PlayerState.playerState.DEATH;
                    mystate.EventStates(PlayerState.myState);
                }
                else
                {                    
                    if(CheckDistancetoEnd() < .8f)
                    {
                        GameManager.instance._isEndGame = true;
                    }
                    #region Move Attack
                    if (Input.GetMouseButtonDown(0)) mymovement.StartMove();

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
                        mymovement.UpdateMove();
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        PlayerState.myState = PlayerState.playerState.COVER;
                        mystate.EventStates(PlayerState.myState);
                        mymovement.EndMove();
                    }
                    #endregion
                }
            }              
        }
    }
}

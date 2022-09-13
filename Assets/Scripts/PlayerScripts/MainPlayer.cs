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
    private bool Accessbility { get; set; }
    private PlayerMovement mymovement;
    private PlayerState mystate;
    [HideInInspector] public CoverCharacter myCover;
    private void Start()
    {
        gun.CreateGun();
        mymovement = GetComponent<PlayerMovement>();
        myCover = GetComponent<CoverCharacter>();
        mystate = new PlayerState(ikComponent, gun, Anim, rigColliders, rigRigidbodies);
        SetIdle();
        GetComponent<Collider>().enabled = true;
        gunRes = gun.GetGunResponse();
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
    private void StartRunning() 
    { myCar.StartRunning(); Accessbility = true; }
    public bool CheckColliderGround()
    {
        if (launchingProjectiles.ins.DistanceTarget() <= .5f)
        {
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

    #region Start Cinematic Events
    public void StartGame()
    {
        SetIdle();
        print(mystate.GetState());
        launchingProjectiles.ins.Launch();
        Anim.SetBool("StartCinematic", CheckColliderGround());
        myCar = EnemyManager.Player.GetComponent<Car>();
        if (myCar)
            Invoke(nameof(StartRunning), 2.5f);
        Invoke(nameof(StartExiting), 2f);
        //play Animation Start Cinematic
    }
    #endregion


    private IEnumerator update(float timer)
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);

            CheckColliderGround();
            Anim.SetBool("StartCinematic", CheckColliderGround());
            if (GameManager.instance.CheckInLoopGame() && Accessbility)
            {                
                if (myhealth.getHealth() < 1)
                {
                    PlayerState.myState = PlayerState.playerState.DEATH;
                    mystate.EventStates(PlayerState.myState);
                }
                else
                {                    
                    if (CheckDistancetoEnd() < 1f)
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

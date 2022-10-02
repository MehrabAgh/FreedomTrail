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
    private LineRenderer Arrower;
    [HideInInspector] public CoverCharacter myCover;
    public bool _isPro;
    public SkinnedMeshRenderer mymodel;
    private void Start()
    {       
        gun.CodeGun = PlayerPrefs.GetInt("Gun");
        if (GameManager.instance.indexLogin <= 1)
            gun.CodeGun = 4;
        gun.CreateGun();
        mymovement = GetComponent<PlayerMovement>();
        myCover = GetComponent<CoverCharacter>();
        Arrower = GetComponentInChildren<LineRenderer>();
        Arrower.gameObject.SetActive(false);
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
    {
        myCar.StartRunning();
        Accessbility = true;
    }
    public bool CheckColliderGround()
    {
        if (launchingProjectiles.ins.DistanceTarget() <= .5f)
        {
            launchingProjectiles.ins.ThisObject.SetParent(launchingProjectiles.ins.TargetObject.parent);
            transform.SetSiblingIndex(0);
            if (!myCar) myCar = GetComponentInParent<Car>();
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
    private void LookatChanging()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), 0.4f);
    }
    public PlayerState.playerState GetState()
    {
        return mystate.GetState();
    }
    #endregion

    #region Start Cinematic Events
    public void StartGame()
    {
        SetIdle();
        launchingProjectiles.ins.Launch();
        Anim.SetBool("StartCinematic", CheckColliderGround());
        myCar = EnemyManager.Player.GetComponent<Car>();
        if (myCar)
        {
            Invoke(nameof(LookatChanging), 1.5f);
            Invoke(nameof(StartRunning), 2.5f);
        }
        Invoke(nameof(StartExiting), 2f);
        //play Animation Start Cinematic
    }
    #endregion


    private IEnumerator update(float timer)
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
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
                        gun.ShootGun();
                        Arrower.gameObject.SetActive(true);
                        PlayerState.myState = PlayerState.playerState.ATTACK;
                        mystate.EventStates(PlayerState.myState);
                        
                        mymovement.UpdateMove();
                    }
                    if (Input.GetMouseButtonUp(0))
                    {
                        PlayerState.myState = PlayerState.playerState.COVER;
                        mystate.EventStates(PlayerState.myState);
                        Arrower.gameObject.SetActive(false);
                        mymovement.EndMove();
                    }
                    #endregion
                }

            }
        }
    }
}

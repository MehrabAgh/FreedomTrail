using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class MainEnemyAI : CharacterMain
    {        
        private EnemyState mystate;
        private EnemyManageSystem Ems;
        public LookAtCharacter lac;
        private float DistanceShoot , startReload,mainReload;        
        private void Start()
        {            
            CreateEnemySystem();
            gun.CreateGun();
            GetComponent<LookAtCharacter>().target = null;
            mystate = GetComponent<EnemyState>();            
            mystate.SetInitialize(ikComponent, gun, Anim, rigColliders, rigRigidbodies, myCar); 
           
            GetComponent<Collider>().enabled = true;
            SetTarget();
            gunRes = gun.GetGunResponse();
            parentAmmo = GameObject.FindGameObjectWithTag("AmmoCollecionEnemy").transform;
            AmmoPooling.instanse.objectToPool = gunRes.bullet;
            AmmoPooling.instanse.Spawning(parentAmmo, gun.Ammos);
            mainReload = gunRes.maxReload;
            startReload = mainReload;
            lac = GetComponent<LookAtCharacter>();            
        }
        #region Helper
        private void CreateEnemySystem()
        {
            Ems = new EnemyManageSystem();

            if (gameObject.name == "P_ProBoss")
                Ems.enemyType = EnemyManageSystem.EnemyType.boss;

            gun.CodeGun = Ems.SelectModeEnemy();
        }
        public void SetReload()
        {
            EnemyState.enemyState = EnemyState.enemystate.RELOAD;
            mystate.EventStates(EnemyState.enemyState);
        }
        public void SetCover()
        {
            if ((Random.Range(0, 3) % 2) != 1)
            {
                EnemyState.enemyState = EnemyState.enemystate.COVER;
                mystate.EventStates(EnemyState.enemyState);
            }
            Invoke("SetAttack", 1f);
        }
        public void SetAttack()
        {            
            EnemyState.enemyState = EnemyState.enemystate.ATTACK;
            mystate.EventStates(EnemyState.enemyState);
        }
        public void SetTarget()
        {
            if(GameManager.instance.Player)
            GetComponent<LookAtCharacter>().target = GameManager.instance.Player.transform;
        }
        public void SetMove()
        {
            EnemyState.enemyState = EnemyState.enemystate.MOVE;
            mystate.EventStates(EnemyState.enemyState);
        }
        public void SetEnd()
        {
            EnemyState.enemyState = EnemyState.enemystate.END;
            mystate.EventStates(EnemyState.enemyState);
        }
        #endregion
        private void FixedUpdate()
        {            
            if (GameManager.instance.CheckInLoopGame())
            {
                if (myhealth.GetHealth() < 1)
                {                    
                    EnemyState.enemyState = EnemyState.enemystate.DEATH;
                    mystate.EventStates(EnemyState.enemyState);
                }
                else if (GameManager.instance.Player.myhealth.GetHealth() <= 0) SetEnd();
                else { 
                lac.target = lac.target != null ? lac.target : GameManager.instance.Player.transform;
                DistanceShoot = Vector3.Distance(transform.position, lac.target.transform.position);                
                if(Ems.enemyType is EnemyManageSystem.EnemyType.boss ||
                    Ems.enemyType is EnemyManageSystem.EnemyType.general && GameManager.instance._isEndLoopGame)
                {                   
                    if (DistanceShoot > 10f) SetMove();
                    else
                    {
                        if (mainReload > 0)
                        {
                            SetAttack();
                            gun.ShootGun();
                            mainReload -= Time.deltaTime;
                        }
                        else
                        {
                            mainReload = startReload;
                            SetReload();
                        }
                    }
                  
                }              
                else if(DistanceShoot < 14f && Ems.enemyType is EnemyManageSystem.EnemyType.general
                    && GetComponentInParent<EnemyCar>() != null)
                {
                    gun.ShootGun();
                    SetAttack();
                }               
            } 
          }
        }      
    }
}
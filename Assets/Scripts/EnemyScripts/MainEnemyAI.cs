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
    
        private void Start()
        {
            CreateEnemySystem();
            gun.CreateGun();
            GetComponent<LookAtCharacter>().target = null;
            mystate = new EnemyState(ikComponent, gun, Anim, rigColliders, rigRigidbodies , myCar);
            GetComponent<Collider>().enabled = true;
            Invoke(nameof(SetTarget),2.5f);
            gunRes = gun.GetGunResponse();
            parentAmmo = GameObject.FindGameObjectWithTag("AmmoCollecionEnemy").transform;
            AmmoPooling.instanse.objectToPool = gunRes.bullet;
            AmmoPooling.instanse.Spawning(parentAmmo, gun.Ammos);
            StartCoroutine(update(0.003f));
        }
        #region Helper
        private void CreateEnemySystem()
        {
            Ems = new EnemyManageSystem();
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
        #endregion
        private IEnumerator update(float timer)
        {
            while (true)
            {
                yield return new WaitForSeconds(timer);
                if (GameManager.instance.CheckInLoopGame())
                {
                    if (myhealth.GetHealth() < 1)
                    {                        
                        EnemyState.enemyState = EnemyState.enemystate.DEATH;
                        mystate.EventStates(EnemyState.enemyState);
                    }
                    else
                    {
                        gun.ShootGun();
                        SetAttack();                       
                    }
                }
            }
        }
    }
}
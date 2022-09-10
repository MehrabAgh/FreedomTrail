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
            Ems = new EnemyManageSystem(this);
            GetComponent<LookAtCharacter>().target = null;
            //GetComponent<LookAtCharacter>().target = GameObject.FindGameObjectWithTag("Player").transform;
            mystate = new EnemyState(ikComponent, gun, Anim, rigColliders, rigRigidbodies);
            AmmoPooling.instanse.objectToPool = gunRes.bullet;
            AmmoPooling.instanse.Spawning(parentAmmo, gun.Ammos);
            GetComponent<Collider>().enabled = true;
            Invoke("SetTarget", 5f);
            StartCoroutine(update(0.003f));
        }
        #region Helper
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
            GetComponent<LookAtCharacter>().target = GameManager.instance.Player.transform;
        }
        #endregion
        private IEnumerator update(float timer)
        {
            while (true)
            {
                yield return new WaitForSeconds(timer);

                if (myhealth.getHealth() < 1)
                {
                    EnemyState.enemyState = EnemyState.enemystate.DEATH;
                    mystate.EventStates(EnemyState.enemyState);
                }
                else
                {
                    if (gun.ShootGun() > -1)
                    {
                        SetAttack();
                    }
                    else
                    {
                        SetReload();
                    }
                }
            }

        }
    }
}
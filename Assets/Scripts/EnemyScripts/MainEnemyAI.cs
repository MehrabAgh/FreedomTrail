using RootMotion.FinalIK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class MainEnemyAI : MonoBehaviour
    {
        [HideInInspector]public mainGun gun;
        [HideInInspector]public GunResponse gunRes;
        public int GunCode = 0;
        private EnemyState mystate;
        private HealthCharacter myhealth;        
        private FullBodyBipedIK ikComponent;
        private Animator Anim;
        private Collider[] rigColliders;
        private Rigidbody[] rigRigidbodies;

        private void Awake()
        {
            rigColliders = GetComponentsInChildren<Collider>();
            rigRigidbodies = GetComponentsInChildren<Rigidbody>();
            gun = GetComponent<mainGun>();
           
            myhealth = GetComponent<HealthCharacter>();
            Anim = GetComponent<Animator>();
            ikComponent = GetComponent<FullBodyBipedIK>();

            gun.CodeGun = GunCode;
        }
        private void Start()
        {         
            GetComponent<LookAtCharacter>().target = GameObject.FindGameObjectWithTag("Player").transform;
            mystate = new EnemyState(ikComponent, gun, Anim, rigColliders, rigRigidbodies);
            gunRes = gun.GetGunResponse();

            AmmoPooling.instanse.objectToPool = gunRes.bullet;            
            AmmoPooling.instanse.Spawning(transform.parent, gun.Ammos);

            SetAttack();

            GetComponent<Collider>().enabled = true;

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
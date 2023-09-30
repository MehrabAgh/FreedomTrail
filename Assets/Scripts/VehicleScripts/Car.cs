using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class Car : MonoBehaviour
    {

        public Transform target;
        public float speed = 5;
        [SerializeField] private float turnSpeed = 0.1f;
        [SerializeField] private float targetProximityDist = 4;
        public int lane;
        [SerializeField] private GameObject[] wheels = new GameObject[2];
        private bool brake = false;
        private HealthCharacter hc;
        private bool isPlayer;
        private CharacterMain PlayerMain;
        [SerializeField] private GameObject explosionVFX, brakeVFX;
        private AudioSource tireSFX;
      

        private void Start()
        {
            GameManager.instance.OnGameOver += StopCarMovement;
            hc = GetComponent<HealthCharacter>();
            if (hc == null)
                hc = GetComponentInChildren<HealthCharacter>();
            isPlayer = GetComponent<EnemyCar>() == null;
            tireSFX = GetComponent<AudioSource>();
            if (isPlayer)
                brake = true;
        }

        private void StopCarMovement()
        {
            brake = true;
            if (isPlayer)
            {
                GetComponent<Animator>().enabled = true;
                GetComponent<AudioSource>().enabled = true;
            }
            //BrakeEffects();
            //StartCoroutine(slowlyStop());

        }

        private void FixedUpdate()
        {
            if (target != null) {
                if (hc != null && hc.GetHealth() <= 0 && PlayerMain == null)
                {

                    PlayerMain = GetComponentInChildren<CharacterMain>();
                    PlayerMain.myhealth.Damage(PlayerMain.myhealth.GetMaxHealth());
                }
                if (brake)
                    return;
                // my gas pedal is stuck down there, welp me :0
                transform.position += transform.forward * speed * Time.deltaTime;

                // I can't seem to stop looking at the target @_@
                var dir = target.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);


                // turn the front wheels to face the target
                foreach (var w in wheels)
                {
                    w.transform.rotation = Quaternion.LookRotation(dir);
                }

                if (!GameManager.instance.IsGameOver &
                    Vector3.Distance(transform.position, target.position) <= targetProximityDist & isPlayer)
                {
                    target = WaypointHolder.instance.nextPoint(target);
                }
            }
        }


        public void StartRunning()
        {
            brake = false;
        }

        public bool GetBrake()
        {
            return brake;
        }

        public void Die()
        {
            if (!isPlayer)
            {
                EnemyCar.EnemyCount--;
                EnemySpawner.instance.takenTargets.Remove(target);
            }
            explosionVFX.SetActive(true);
            explosionVFX.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject, 5);
            target = transform;
            if (isPlayer) GameManager.instance.Player.SetIdle();
            speed = 0;
            hc = null;
        }
      
        public void BrakeEffects()
        {
            tireSFX.Play();
            var fx = Instantiate(brakeVFX, transform);
            fx.SetActive(true);
            Destroy(fx, 1.6f);

        }

    }
}

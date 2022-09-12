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

        private bool isPlayer;

        [SerializeField] private GameObject explosionVFX, brakeVFX;
        private AudioSource tireSFX;

        private void Start()
        {
            GameManager.instance.OnGameOver += StopCarMovement;
            isPlayer = GetComponent<EnemyCar>() == null;
            tireSFX = GetComponent<AudioSource>();
            brake = true;
        }

        private void StopCarMovement()
        {
            brake = true;
            //BrakeEffects();
            //StartCoroutine(slowlyStop());

        }

        private void FixedUpdate()
        {
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


        public void StartRunning()
        {
            brake = false;
        }

        public void Die()
        {
            if (!isPlayer)
            {
                EnemyCar.EnemyCount--;
                EnemySpawner.instance.takenTargets.Remove(target);
            }

            Instantiate(explosionVFX, transform.position, Quaternion.identity);
            Destroy(gameObject, 5);
            target = transform;
            StartCoroutine(slowlyStop());


        }

        private IEnumerator slowlyStop()
        {
            while (true)
            {
                speed = Mathf.Lerp(speed, 0, 0.01f);
                yield return null;
            }
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

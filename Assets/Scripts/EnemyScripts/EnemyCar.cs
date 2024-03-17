using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class EnemyCar : MonoBehaviour
    {

        public Car _car;
        [SerializeField] private float closeRange = 5;
        private float playerSpeed;
        private bool close;
        public static int EnemyCount = 0;



        private void Start()
        {
            EnemyCount++;
            _car = GetComponent<Car>();
            playerSpeed = EnemyManager.Player.GetComponent<Car>().speed;
            _car.speed = playerSpeed * 1.15f;

        }

        private void Update()
        {            
            if (GameManager.instance.IsGameOver)
            {
                GetComponent<Car>().enabled = false;
                GetComponent<EnemyCar>().enabled = false;
            }
            if (EnemyManager.Player != null)
            {
                if (Vector3.Distance(transform.position, EnemyManager.Player.transform.position) <= closeRange)
                {
                    StartCoroutine(UpdateSpeed());
                }
            }

        }

        private IEnumerator UpdateSpeed()
        {
            if (close)
            {
                _car.speed = playerSpeed-2;
            }
            else
            {

                _car.speed = playerSpeed * 0.9f;
                _car.BrakeEffects();
                yield return new WaitForSeconds(0.5f);
                close = true;
                while (true)
                {
                    _car.speed = Mathf.Lerp(_car.speed, playerSpeed * 1.15f, 0.01f);
                    yield return null;
                }

            }
        }
    }


}

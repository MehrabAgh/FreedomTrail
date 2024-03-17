using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class AmmoLife : MonoBehaviour
    {
        public int Damage;
        private void Destroyed()
        {
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            Invoke(nameof(Destroyed), 0.2f);
        }
        private void OnTriggerEnter(Collider other)
        {
            var tag = other.gameObject.tag;

            switch (tag)
            {

                case "Player":
                    {
                        if (transform.parent.CompareTag("AmmoCollecionEnemy"))
                            other.GetComponent<HealthCharacter>().Damage(Damage);
                        break;
                    }
                case "PlayerCar":
                    {
                        if (transform.parent.CompareTag("AmmoCollecionEnemy"))
                            other.GetComponent<HealthCharacter>().Damage(Damage);
                        break;
                    }
                case "EnemyCar":
                    {
                        if (!transform.parent.CompareTag("AmmoCollecionEnemy"))
                            other.GetComponent<HealthCharacter>().Damage(Damage);
                        break;
                    }
                case "Enemy":
                    {
                        if (!transform.parent.CompareTag("AmmoCollecionEnemy"))
                            other.GetComponent<HealthCharacter>().Damage(Damage);
                        break;
                    }
                case "Cover":
                    {
                        if (transform.parent.CompareTag("AmmoCollecionEnemy"))
                        {
                            Destroyed();
                            other.GetComponent<HealthCharacter>().Damage(Damage);
                        }
                        break;
                    }
                case "CoverEnding":
                    {
                        Destroyed();
                        break;
                    }
                case "ScorePickup":
                    {
                        if (!transform.parent.CompareTag("AmmoCollecionEnemy"))
                            other.GetComponent<CoinPickup>().GetPicked();
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs {
    public class AmmoLife : MonoBehaviour
    {
        public int Damage;
        private void Destroyed()
        {
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            Invoke("Destroyed", 0.5f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<HealthCharacter>().Damage(Damage);
            }
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<HealthCharacter>().Damage(Damage);
            }
            if (other.gameObject.tag == "Cover")
            {
                Destroyed();
            }
        }
    }
}

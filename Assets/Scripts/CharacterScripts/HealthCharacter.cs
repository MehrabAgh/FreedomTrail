using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vino.Devs
{
    public class HealthCharacter : MonoBehaviour
    {
        private int health;
        [SerializeField] private int maxHealth;
        private void Awake()
        {
            health = maxHealth;
        }
        public void Damage(int takeHealth)
        {
            health -= takeHealth;            
        }
        public int GetHealth()
        {
            return health;
        }
        public int GetMaxHealth()
        {
            return maxHealth;
        }
    }
}

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
            //fx
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
                        if (transform.parent.CompareTag("AmmoCollecionEnemy")){
                            other.GetComponent<HealthCharacter>().Damage(Damage);                        
							ParticleManager.instanse.Spawn(ParticleManager.instanse.CharHitFx, transform, .7f);							
							Destroyed();
						}
                        break;
                    }
                case "PlayerCar":
                    {
                        if (transform.parent.CompareTag("AmmoCollecionEnemy")){
                            other.GetComponent<HealthCharacter>().Damage(Damage);							
							ParticleManager.instanse.Spawn(ParticleManager.instanse.ObjHitFx, transform, .7f);
							Destroyed();
						}                        
                        break;
                    }
                case "EnemyCar":
                    {
                        if (!transform.parent.CompareTag("AmmoCollecionEnemy")){
                            other.GetComponent<HealthCharacter>().Damage(Damage);
							ParticleManager.instanse.Spawn(ParticleManager.instanse.ObjHitFx, transform, .7f);
							Destroyed();
						}
                        break;
                    }
                case "Enemy":
                    {
                        if (!transform.parent.CompareTag("AmmoCollecionEnemy")){
                            other.GetComponent<HealthCharacter>().Damage(Damage);							
							ParticleManager.instanse.Spawn(ParticleManager.instanse.CharHitFx, transform, .7f);
							Destroyed();
						}
                        break;
                    }
                case "Cover":
                    {
                        if (transform.parent.CompareTag("AmmoCollecionEnemy"))
                        {                            
                            other.GetComponent<HealthCharacter>().Damage(Damage);
                            ParticleManager.instanse.Spawn(ParticleManager.instanse.ObjHitFx, transform, .7f);
							Destroyed();
                        }
                        break;
                    }
                case "CoverEnding":
                    {                        
                        ParticleManager.instanse.Spawn(ParticleManager.instanse.ObjHitFx, transform, .7f);
						Destroyed();
                        break;
                    }
                case "ScorePickup":
                    {
                        if (!transform.parent.CompareTag("AmmoCollecionEnemy")){							
                            other.GetComponent<CoinPickup>().GetPicked();
							ParticleManager.instanse.Spawn(ParticleManager.instanse.ObjHitFx, transform, .7f);
							Destroyed();
						}
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
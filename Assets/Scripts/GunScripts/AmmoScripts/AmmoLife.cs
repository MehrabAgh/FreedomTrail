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
		    var tag = other.gameObject.tag;
		    
		    switch (tag)
		    {
		    
		    case "Player":
			    {
				    other.GetComponent<HealthCharacter>().Damage(Damage);
				    break;
			    }
		    case "Enemy":
			    {
				    other.GetComponent<HealthCharacter>().Damage(Damage);
				    break;
			    }
		    case "Cover":
			    {
				    Destroyed();
			    	
				    break;
			    }
		    case"ScorePickup":
			    {
			    	other.GetComponent<CoinPickup>().GetPicked();
				    break;
			    }
		    
		    default:
		    	break;
		    }
        
		    /*
	        if (other.gameObject.CompareTag("Player"))
            {
                other.GetComponent<HealthCharacter>().Damage(Damage);
            }
	        if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<HealthCharacter>().Damage(Damage);
            }
	        if (other.gameObject.CompareTag("Cover"))
            {
                Destroyed();
		    }*/
            
        }
    }
}

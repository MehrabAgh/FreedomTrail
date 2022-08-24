using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItemBox : MonoBehaviour
{
    public float MaxHealth;
    private float health;
    public int MaxCoin;
    private void Start()
    {        
        health = MaxHealth;       
    }
    private void Update()
    {
        if (health <= 0)
        {
            for (int i = 0; i < MaxCoin; i++)
            {
                Instantiate(ScoreManager.instance.CoinObj, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {        
        if (collision.gameObject.tag == "AmmoPlayer")
        {
            health--;  
        }
    }
}

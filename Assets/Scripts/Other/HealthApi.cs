using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HealthApi : MonoBehaviour
{
    [FormerlySerializedAs("MaxHealth")] public float maxHealth;
    private float health;
    public Slider healthBar; 
    
    //visuals
    public GameObject damagedMesh;
    public GameObject explosionFX;
    private void Start()
    {
    
        healthBar.maxValue = maxHealth;
        health = maxHealth;
        healthBar.value = maxHealth;
    }
    private void Update()
    {
        if (health <= 0)
        {
            //GameOver            
            if (gameObject.CompareTag("Player"))
            {
                //PlayerDeath
                GameManager.ins.UIOver.SetActive(true);
                GameManager.ins.UIGame.SetActive(false);
                GameManager.ins.UIFinish.SetActive(false);
                GameManager.ins._isEndGame = true;                             
            }
            if (gameObject.CompareTag("Enemy"))
            {
                Instantiate(ScoreManager.instance.CoinObj, transform.position, transform.rotation);
                Destroy(gameObject);
                Instantiate(explosionFX, transform.position, Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("AmmoEnemy"))
        {
            if (gameObject.CompareTag("Player"))
            {
                health--;
                StartCoroutine(FlickerDamageEffect());
                healthBar.value = health;
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.CompareTag("AmmoPlayer"))
        {
            if (gameObject.CompareTag("Enemy"))
            {
                health--;
                StartCoroutine(FlickerDamageEffect());
                healthBar.value = health;
                Destroy(collision.gameObject);
            }
        }
    }

    private IEnumerator FlickerDamageEffect()
    {

        for (int i = 0; i < 4; i++)
        { 
            damagedMesh.SetActive(!damagedMesh.activeInHierarchy);
            yield return new WaitForSeconds(0.2f);
        }
        damagedMesh.SetActive(true);
    }
}

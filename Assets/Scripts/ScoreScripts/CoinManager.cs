using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    bool c;
    private void Update()
    {
        if (gameObject.name == "Coin(Clone)" || c)
        {
            transform.SetParent(GameManager.instance.Player.transform);
            transform.position = Vector3.Lerp(transform.position, GameManager.instance.Player.transform.position, Time.deltaTime * 3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ScoreManager.instance.Coin++;
            PlayerPrefs.SetInt("Coin", ScoreManager.instance.Coin);
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "AmmoPlayer")
        {
            c = true;
        }
    }
}

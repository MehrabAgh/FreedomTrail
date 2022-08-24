using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinImageDist : MonoBehaviour
{
    private GiftPie gp;
    public float dis;
    private void Start()
    {
        gp = FindObjectOfType<GiftPie>();
    }
    void Update()
    {
        dis = Vector3.Distance(transform.position, gp.pivCoin.transform.position);
        if (dis <= 30f)
        {
            ScoreManager.instance.CoinStart += 1;
            PlayerPrefs.SetInt("CoinStart", ScoreManager.instance.CoinStart);
            Destroy(gameObject);
        }
    }
}

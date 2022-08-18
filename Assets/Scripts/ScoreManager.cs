using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public GameObject CoinObj;
    public int Coin , CoinStart;
    public int Kill;
    public Text[] TcoinSubmited , Tcoin,TLevel;
    private void Awake()
    {     
        instance = this;                  
    }
    private void Update()
    {
        CoinStart = PlayerPrefs.GetInt("CoinStart");
        foreach (Text item in TcoinSubmited)
        {
            item.text = CoinStart.ToString();
        }
        foreach (Text item in Tcoin)
        {
            item.text = Coin.ToString();
        }
    }
}

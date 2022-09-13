using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;    
	public int Coin , Key , Xp;      
    
	// coin spawning
	public GameObject[] pickupPrefabs;
    
    private void Awake()
    {     
        instance = this;                  
    } 
    
	private void Start()
	{
		StartCoroutine(SpawnCoin());
	}
    
	private IEnumerator SpawnCoin()
	{
		yield return null;
	}
}

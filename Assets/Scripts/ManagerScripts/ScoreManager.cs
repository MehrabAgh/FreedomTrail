using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vino.Devs;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;    
	public int Coin , Key , Xp;      
    
	// coin spawning
	[SerializeField] private GameObject[] pickupPrefabs;
	public float spawnRate = 5; // the time between each spawning 
	[SerializeField] private float posRandRadius = 3; 
	
	private Transform playerVehicle;
    
	private void Awake() => instance = this;
    
	private void Start()
	{
		playerVehicle = GameObject.FindGameObjectWithTag("PlayerCar").transform;
		StartCoroutine(SpawnCoin());
	}
    
	private IEnumerator SpawnCoin()
	{
		while (true)
		{
			if(GameManager.instance._isStartGame && !GameManager.instance.IsGameOver)
			{
				yield return new WaitForSeconds(spawnRate);
				var radius = Random.Range(posRandRadius * 0.4f, posRandRadius);
				radius *= Random.Range(0,2)*2-1;
				
				Instantiate(pickupPrefabs[Random.Range(0, pickupPrefabs.Length)], 
					playerVehicle.position + new Vector3(radius, 1.5f, radius),
					Quaternion.identity);
				
				/*
				Instantiate(pickupPrefabs[Random.Range(0, pickupPrefabs.Length)], 
					WaypointHolder.instance.GetClosestPoint(GameManager.instance.Player.transform).position + 
					new Vector3(radius, 1, radius), Quaternion.identity);
				yield return null;*/
			}
			else yield return null;
		}
	}
}


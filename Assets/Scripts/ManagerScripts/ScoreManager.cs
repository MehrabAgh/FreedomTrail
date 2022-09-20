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
	[SerializeField] private GameObject pickupTrailEffect;
	public RectTransform coinUi;
	public float spawnRate = 5; // the time between each spawning 
	public int spawnLimit = 5; // how many score pickups can be spawned in a level
	[SerializeField] private float posRandRadius = 3; 
	
	private bool initDelayed;
	
	private Transform playerVehicle;
    
	private void Awake() => instance = this;
    
	private void Start()
	{
		playerVehicle = GameObject.FindGameObjectWithTag("PlayerCar").transform;
		StartCoroutine(SpawnCoin());
	}
    
	private IEnumerator SpawnCoin()
	{
		while (spawnLimit > 0)
		{
			if(GameManager.instance.CheckInLoopGame())
			{
				if(!initDelayed)
				{
					yield return new WaitForSeconds(4.5f);
					initDelayed = true;
				}
				yield return new WaitForSeconds(spawnRate);
				var radius = Random.Range(posRandRadius * 0.4f, posRandRadius);
				radius *= Random.Range(0,2)*2-1;
				
				Instantiate(pickupPrefabs[Random.Range(0, pickupPrefabs.Length)], 
					playerVehicle.position + new Vector3(radius, 1.5f, radius),
					Quaternion.identity);
				spawnLimit--;
			}
			else yield return null;
		}
	}
	
	public void coin2uiFx(Vector3 coinPos, int count = 1) => StartCoroutine(coin2ui(coinPos, count));
	
	private IEnumerator coin2ui(Vector3 coinPos, int count)
	{
		for (int i = 0; i < count; i++) 
		{
			StartCoroutine(spawnEffect(coinPos));
			yield return new WaitForSeconds(0.1f);
		}
	}
	private IEnumerator spawnEffect(Vector3 coinPos)
	{
		Transform fx = Instantiate(pickupTrailEffect, Vector3.zero, Quaternion.identity).transform;
		fx.position = coinPos;
		Destroy(fx.gameObject, 1);
		Vector3 coinWorldPos = Camera.main.ScreenToWorldPoint(coinUi.position + new Vector3(0,0,50));
		for (int j = 0; j < 50; j++) 
		{
			fx.position = Vector3.Lerp(fx.position, coinWorldPos, 0.1f);
			yield return null;
		}
	}
}


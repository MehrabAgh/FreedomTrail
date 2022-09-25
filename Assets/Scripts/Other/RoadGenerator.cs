using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

public class RoadGenerator : MonoBehaviour 
{
	[SerializeField] private GameObject[] RoadModularParts;
	private Transform lastModularPoint;
	private int lastRandIndex;
	[SerializeField] private int spawnCount = 5;
	[SerializeField] private bool infiniteRoad = false;
	private Coroutine infiniteRoadGeneration;
	
	public static RoadGenerator ins;
	
	private void Awake () 
	{
		if(infiniteRoad)
			infiniteRoadGeneration = StartCoroutine(SpawnRoadInfinite());
		else
			SpawnRoad(spawnCount);
			
		ins = this;
	}
	
	private void SpawnRoad(int count)
	{
		for (int i = 0; i < count; i++) 
		{
			var rand = Random.Range(0, RoadModularParts.Length);
			var randIndex = Mathf.Clamp(rand == lastRandIndex? rand + lastRandIndex: rand, 0, RoadModularParts.Length);
			var curRoadPart = Instantiate(RoadModularParts[randIndex], transform);
			if(lastModularPoint == null)
			{
				curRoadPart.transform.position = Vector3.zero;
				lastModularPoint = curRoadPart.transform.GetChild(0).transform;
			}
			else
			{
				curRoadPart.transform.position = lastModularPoint.position;
				curRoadPart.transform.rotation = lastModularPoint.rotation;
				Destroy(lastModularPoint.gameObject);
				lastModularPoint = curRoadPart.transform.GetChild(0).transform;
			}
			var parent = WaypointHolder.instance.transform;
			for (int j = 1; j <= curRoadPart.transform.childCount + 1; j++) 
			{
				curRoadPart.transform.GetChild(1).transform.parent = parent;
			}
			curRoadPart.name = "Road Part";
		}
	}
	
	private IEnumerator SpawnRoadInfinite(float delay = 7)
	{
		while(true)
		{
			SpawnRoad(spawnCount);
			Debug.Log(lastModularPoint.position);
			yield return new WaitForSeconds(delay);
		}
	}
	
	public void StopInfinitRoadGeneration()
	{
		StopCoroutine(infiniteRoadGeneration);
	}
	
	#region debugging
	private void Update () 
	{
		if(Input.GetButtonDown("Jump")) // Press Space to regenerate a random road
		{
			clean();
			SpawnRoad(spawnCount);
		}
	}
	
	private void clean()
	{
		foreach (Transform item in transform)
		{
			Destroy(item.gameObject);
		}
		lastModularPoint = null;
	}
	#endregion
}

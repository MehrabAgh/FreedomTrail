using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

public class RoadGenerator : MonoBehaviour 
{
	[SerializeField] private GameObject[] RoadModularParts;
	[SerializeField] private int spawnCount = 5;
	private Transform lastModularPoint;
	
	private void Awake () 
	{
		SpawnRoad(spawnCount);
	}
	
	private void SpawnRoad(int count)
	{
		for (int i = 0; i < count; i++) 
		{
			var randIndex = Random.Range(0, RoadModularParts.Length);
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
				lastModularPoint = curRoadPart.transform.GetChild(0).transform;
			}
			var parent = WaypointHolder.instance.transform;
			for (int j = 1; j <= curRoadPart.transform.childCount + 1; j++) 
			{
				curRoadPart.transform.GetChild(1).transform.parent = parent;
			}
			Destroy(curRoadPart.transform.GetChild(0).gameObject);
			curRoadPart.name = "Road Part";
		}
	}
	
	// Debugging
	private void Update () 
	{
		if(Input.GetButtonDown("Jump"))
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{	
	public static GameObject Player;	
	private void Awake()
	{
		Player = GameObject.FindGameObjectWithTag("PlayerCar");
	}
}

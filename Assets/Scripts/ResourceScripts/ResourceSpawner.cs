using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;

public class ResourceSpawner : MonoBehaviour
{
    // coin spawning
    [SerializeField] private GameObject[] pickupPrefabs;
    [SerializeField] private GameObject pickupTrailEffect;
    public RectTransform coinUi;
    public float MainSpwnRate;
    private float spawnRate ; // the time between each spawning 
    [SerializeField] private float posRandRadius = 3;
    public static ResourceSpawner instance;
    private Transform playerVehicle;    
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        spawnRate = MainSpwnRate;   
        playerVehicle = GameObject.FindGameObjectWithTag("PlayerCar").transform;
        if (LevelManager.instance.levelMode == LevelManager.LevelMode.Bonus)
            FirstSpawnCoin();       
    }
    private void FixedUpdate()
    {
        if (GameManager.instance._isStartGame)
        {
            if (spawnRate <= 0)
                SpawnCoin();
            else spawnRate -= Time.deltaTime;
        }
    }
    private void SpawnCoin()
    {
        if (GameManager.instance.CheckInLoopGame())
        {            
            var radius = Random.Range(posRandRadius * 0.4f, posRandRadius);
            radius *= Random.Range(0, 2) * 2 - 1;

            Instantiate(pickupPrefabs[Random.Range(0, pickupPrefabs.Length)],
                playerVehicle.position + new Vector3(radius, 1.5f, radius),
                Quaternion.identity);

            spawnRate = MainSpwnRate;
        }
        else this.enabled = false;
    }
    public void Coin2uiFx(Vector3 coinPos) => StartCoroutine(coin2ui(coinPos));
    private void FirstSpawnCoin()
    {
        for(int i = 0; i < (Random.Range(30 , 60)); i++)
        {
            int _x = Random.Range(-19,19);
            int _z = Random.Range(-26,-300);
            Instantiate(pickupPrefabs[Random.Range(0, pickupPrefabs.Length)]
                    ,new Vector3(_x,2,_z),Quaternion.identity);
        }
        //foreach (Transform item in points)
        //{
        //    int _state = Random.Range(0,points.Count);
        //    if ((_state % 2) == 0)
        //    {
        //        Instantiate(pickupPrefabs[Random.Range(0, pickupPrefabs.Length)]
        //            ,item.position,Quaternion.identity);
        //    }            
        //}
    }
    private IEnumerator coin2ui(Vector3 coinPos)
    {
        Transform fx = Instantiate(pickupTrailEffect, Vector3.zero, Quaternion.identity).transform;
        fx.position = coinPos;
        Destroy(fx.gameObject, 1);
        Vector3 coinWorldPos = Camera.main.ScreenToWorldPoint(coinUi.position + new Vector3(0, 0, 10));

        while (true)
        {
            if (fx != null && coinWorldPos != null)
            {
                fx.position = Vector3.Lerp(fx.position, coinWorldPos, 0.1f);               
            }
            yield return null;
        }
    }
}

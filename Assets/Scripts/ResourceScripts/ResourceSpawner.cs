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
    public float spawnRate = 5; // the time between each spawning 
    [SerializeField] private float posRandRadius = 3;
    public static ResourceSpawner instance;
    private Transform playerVehicle;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        playerVehicle = GameObject.FindGameObjectWithTag("PlayerCar").transform;
        StartCoroutine(SpawnCoin());
    }
    private IEnumerator SpawnCoin()
    {
        while (true)
        {
            if (GameManager.instance.CheckInLoopGame())
            {
                yield return new WaitForSeconds(spawnRate);
                var radius = Random.Range(posRandRadius * 0.4f, posRandRadius);
                radius *= Random.Range(0, 2) * 2 - 1;

                Instantiate(pickupPrefabs[Random.Range(0, pickupPrefabs.Length)],
                    playerVehicle.position + new Vector3(radius, 1.5f, radius),
                    Quaternion.identity);
            }
            else
            {
                this.enabled = false;
                yield return null;
            }
        }
    }
    public void Coin2uiFx(Vector3 coinPos) => StartCoroutine(coin2ui(coinPos));

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
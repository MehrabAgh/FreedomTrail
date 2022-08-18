using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    private int aliveEnemies;
    public Transform[] spawnPoint; // the spawn whereabouts
    public Transform  submitSpawnPoint;
    public GameObject spawnFX;
    public GameObject[] enemyPrefabs;
    private bool spawnAllowed;
    public static EnemySpawner ES; // instance holder
    public int maxEnemy;

    private void Awake()
    {
        ES = this;
    }
    private void Start()
    {      
        spawnAllowed = true;
       // Spawn();
        StartCoroutine(spawn());
    }

    private void Update()
    {
        aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
       // spawnAllowed = aliveEnemies < 2; // if there's < 2 enemies alive we can spawn new ones
    }
    private void Spawn()
    {
        for (int i = 0; i < maxEnemy; i++)
        {
            // wait for some seconds before spawning new enemies      
            submitSpawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];

            Vector3 pos = submitSpawnPoint.position + (Vector3.right * Random.Range(-1, 1) * 5);

            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], pos,
            Quaternion.Euler(new Vector3(0, 180, 0)));
        }
    }
    private IEnumerator spawn()
    {

        for (int i = 0; i < maxEnemy; i++)
        {
            if (spawnAllowed)
            {
                //print("spawning");

                // wait for some seconds before spawning new enemies
                float delay = Random.Range(1, 3);
                submitSpawnPoint = spawnPoint[Random.Range(0, spawnPoint.Length)];
                yield return new WaitForSeconds(delay);



                Vector3 pos = submitSpawnPoint.position + (Vector3.right * Random.Range(-1, 1) * 5);

                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], pos,
                Quaternion.Euler(new Vector3(0, 180, 0)));

                //Instantiate(spawnFX, pos, Quaternion.identity); // spawn effect

            }
            else
            {
                yield return null;
            }
        }
    }

}

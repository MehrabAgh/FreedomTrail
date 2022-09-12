using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Vino.Devs
{
    [RequireComponent(typeof(EnemyManager))]
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner instance;
        public int MaxEnemies = 3;
        public float spawnRate = 5;
        private Transform spawnPoint;
        private List<Transform> playerFollowTargets = new List<Transform>();
        public List<Transform> takenTargets = new List<Transform>();

        [SerializeField] private GameObject[] EnemyPrefabs;

        private void Awake() => instance = this;

        private void Start()
        {
            StartCoroutine(SpawnLoop());
            spawnPoint = GameObject.Find("enemy spawn point").transform;
            var holder = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
            for (int i = 0; i < holder.childCount; i++)
            {
                playerFollowTargets.Add(holder.GetChild(i).transform);
            }
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnRate);
                if (EnemyCar.EnemyCount < MaxEnemies ) // Hey mehrab add  & GameManager.game hanooz shoro nashode  here
                {
                    Spawn();
                }

                if (GameManager.instance.IsGameOver)
                    break;
                else yield return null;
            }
        }

        private void Spawn()
        {

            Car enemy = Instantiate(EnemyPrefabs[0], spawnPoint.position, spawnPoint.rotation).GetComponent<Car>();
            enemy.target = randTarget();
        }


        private Transform randTarget()
        {
            int randIndex;
            do
            {
                randIndex = Random.Range(0, playerFollowTargets.Count);
            } while (takenTargets.Contains(playerFollowTargets[randIndex]));

            takenTargets.Add(playerFollowTargets[randIndex]);
            return playerFollowTargets[randIndex];


        }

    }

}
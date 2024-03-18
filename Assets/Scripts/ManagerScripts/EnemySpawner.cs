using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
namespace Vino.Devs
{
    [RequireComponent(typeof(EnemyManager))]
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner instance;
        public int MaxEnemies = 3;
        public float spawnRate = 2;
        [SerializeField]private List<Transform> spawnPoint;
        [SerializeField] private List<Transform> spawnEndingPoint;
        private List<Transform> playerFollowTargets = new List<Transform>();
        public List<Transform> takenTargets = new List<Transform>();

        [SerializeField] private GameObject[] EnemyPrefabs;
        [SerializeField] private GameObject EnemyCharPrefab;
        private void Awake() => instance = this;

        private void Start()
        {
            EnemyCar.EnemyCount = 0;
            StartCoroutine(SpawnLoop());            
            var holder = GameObject.FindGameObjectWithTag("EnemyTarget").transform;
            for (int i = 0; i < holder.childCount; i++)
            {
                playerFollowTargets.Add(holder.GetChild(i).transform);
            }
        }
        
        private void FixedUpdate()
        {
            if (GameManager.instance._isEndLoopGame)
            {
                StartCoroutine(SpawnEndLoop());
            }
        }

        private IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnRate);
                if (GameManager.instance._isEndLoopGame)
                    break;
                if (EnemyCar.EnemyCount < MaxEnemies  && GameManager.instance.OnRealGame)
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
            var pos = spawnPoint[Random.Range(0, spawnPoint.Count)];
            //ParticleManager.instanse.Spawn(ParticleManager.instanse.Spawn_Enemy , pos , 3);
            Car enemy = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)], pos.position
                , pos.rotation).GetComponent<Car>();
            ParticleManager.instanse.EnableEffect(enemy.GetComponent<ParticleItem>().Spawn);
            GameManager.instance.SpawnedEnemyCars.Add(enemy.GetComponentInParent<EnemyCar>());
            enemy.target = RandTarget();
        }
        private Transform RandTarget()
        {
            int randIndex;
            do
            {
                randIndex = Random.Range(0, playerFollowTargets.Count);
            } while (takenTargets.Contains(playerFollowTargets[randIndex]));

            takenTargets.Add(playerFollowTargets[randIndex]);
            return playerFollowTargets[randIndex];
        }
        
        public IEnumerator SpawnEndLoop()
        {
            foreach (var item in GameManager.instance.EnderEnemyCars)
            {
                yield return new WaitForSeconds(spawnRate);
                if(item.gameObject != null)
                    item.gameObject.SetActive(true);
            }          
        }

        public void SpawnEnding()
        {
            var rand = Random.Range(1, 7);
            for (int i = 0; i < rand; i++)
            {
                MainEnemyAI enemy = Instantiate(EnemyCharPrefab, spawnEndingPoint[Random.Range(0, spawnEndingPoint.Count)].position
                          , spawnEndingPoint[Random.Range(0, spawnEndingPoint.Count)].rotation).GetComponent<MainEnemyAI>();
                GameManager.instance.EnderEnemyCars.Add(enemy);
                //enemy.lac.target = GameManager.instance.Player.transform;
                enemy.gameObject.SetActive(false);
            }           
        }

    }

}
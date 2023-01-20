using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField][Range(0.1f, 4f)] private float spawnInterval;
    [SerializeField] private int groupMinSize = 2;
    [SerializeField] private int groupMaxSize = 5;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool allowSpawning;

    private readonly int objectPoolMaxCapacity = 400;
    private readonly int objectPoolDefaultCapacity = 400;
    private readonly System.Random random = new System.Random();
    private int spawnCounter = 0;
    private ObjectPool<Enemy> enemyPool;
    private Coroutine spawnRoutine;

    private void Awake()
    {
        enemyPool = new ObjectPool<Enemy>(
            CreateEnemy,
            OnGetEnemy,
            OnReleaseEnemy,
            OnDestroyObjectEnemy,
            false,
            objectPoolDefaultCapacity,
            objectPoolMaxCapacity);
    }

    private IEnumerator SpawnRoutine()
    {
        while (allowSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            foreach (Transform spawnPoint in ChooseRandomSpawnPoints())
            {
                Enemy enemy = enemyPool.Get();
                enemy.transform.position = spawnPoint.position;
                enemy.gameObject.SetActive(true);
            };
        }
    }

    private IEnumerable<Transform> ChooseRandomSpawnPoints()
    {
        return spawnPoints.OrderBy(x => random.Next()).Take(UnityEngine.Random.Range(groupMinSize, groupMaxSize + 1));
    }

    public void BeginSpawning()
    {
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
    }

    private Enemy CreateEnemy()
    {
        Enemy instance = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        spawnCounter++;
        instance.name = spawnCounter.ToString();
        instance.Disable += ReturnObjectToPool;
        return instance;
    }

    private void OnGetEnemy(Enemy instance) { }

    private void OnReleaseEnemy(Enemy instance) => instance.gameObject.SetActive(false);

    private void OnDestroyObjectEnemy(Enemy instance) => Destroy(instance.gameObject);

    private void ReturnObjectToPool(Enemy instance) => enemyPool.Release(instance);
}

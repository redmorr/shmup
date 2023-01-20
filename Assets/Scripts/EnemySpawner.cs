using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private bool allowSpawning;

    private readonly int objectPoolMaxCapacity = 200;
    private readonly int objectPoolDefaultCapacity = 200;
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
            yield return new WaitForSeconds(2f);
            enemyPool.Get();
        }
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

    private Vector3 GetRandomSpawnPoint() => spawnPoints[Random.Range(0, spawnPoints.Length)].position;

    private Enemy CreateEnemy()
    {
        Enemy instance = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
        spawnCounter++;
        instance.name = spawnCounter.ToString();
        instance.Disable += ReturnObjectToPool;
        instance.gameObject.SetActive(false);
        return instance;
    }

    private void OnGetEnemy(Enemy instance)
    {
        instance.transform.position = GetRandomSpawnPoint();
        instance.gameObject.SetActive(true);
    }

    private void OnReleaseEnemy(Enemy instance) => instance.gameObject.SetActive(false);

    private void OnDestroyObjectEnemy(Enemy instance) => Destroy(instance.gameObject);

    private void ReturnObjectToPool(Enemy instance) => enemyPool.Release(instance);
}

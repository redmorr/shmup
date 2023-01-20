using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private bool allowSpawning;

    private Coroutine spawnRoutine;

    private void Start()
    {
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (allowSpawning)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPoint() => spawnPoints[Random.Range(0, spawnPoints.Length)].position;
}

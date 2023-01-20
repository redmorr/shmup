using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private bool allowSpawning;

    private Coroutine spawnRoutine;

    private IEnumerator SpawnRoutine()
    {
        while (allowSpawning)
        {
            yield return new WaitForSeconds(2f);
            Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
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
}

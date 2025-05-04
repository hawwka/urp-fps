using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public float SpawnRadius = 50f;
    public int MaxEnemies = 10;

    public Transform Runtime;
    public Transform PlayerPosition;
    public GameObject EnemyPrefab;

    int enemiesCount;
    
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (gameObject)
        {
            yield return new WaitForSeconds(3f);
            if (enemiesCount >= MaxEnemies) 
                continue;
            
            var spawnPosition = PlayerPosition.position + (Random.insideUnitSphere * SpawnRadius);
            spawnPosition.y = 0;
            var enemy = Instantiate(EnemyPrefab, spawnPosition, quaternion.identity);
            enemy.transform.SetParent(Runtime);
            enemy.GetComponent<Health>().OnHealthEnd += () => enemiesCount--;
        }
    }
}


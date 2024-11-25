using System;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private Transform[] spawnPoint;
    public GameObject enemyPrefab;

    private void Start()
    {
        Invoke(nameof(SpawnEnemy), 0.1f);
    }

    public void SpawnEnemy()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawnPoint");
        
        foreach (GameObject spawnPoint in spawnPoints)
        {
            Vector3 spawnPosition = spawnPoint.transform.position;
            Quaternion spawnRotation = spawnPoint.transform.rotation;
            
            Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public Transform[] spawnPoints;
    public float spawnTime;
    public GameObject enemy;
    public int enemyLimit;
    public int spawnedEnemies;
	// Use this for initialization
	void Start ()
    {
        spawnedEnemies = 0;
        InvokeRepeating("Spawn", spawnTime, spawnTime);
	}
	
    void Spawn()
    {
        if (spawnedEnemies < enemyLimit)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject spawned = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            spawnedEnemies++;
        }
    }
    
}

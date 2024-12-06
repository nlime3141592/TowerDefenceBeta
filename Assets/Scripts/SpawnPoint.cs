using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnPoint
{
    public int spawnPointIndex;

    public float spawnCooltime;

    public List<Enemy> enemyPrefabs;

    private float leftSpawnCooltime;

    public void OnUpdate()
    {
        leftSpawnCooltime -= Time.deltaTime;
    }

    public Enemy GenerateEnemyOrNull()
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0 || leftSpawnCooltime > 0.0f)
            return null;

        System.Random prng = new System.Random();

        leftSpawnCooltime = spawnCooltime;

        int index = prng.Next(enemyPrefabs.Count);
        return GameObject.Instantiate<Enemy>(enemyPrefabs[index]);
    }
}
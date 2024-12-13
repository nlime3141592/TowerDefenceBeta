using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnPointPhase
{
    public float spawnCooltime;
    public List<Enemy> enemyPrefabs;

    public Enemy GenerateRandomEnemyOrNull()
    {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0)
            return null;

        System.Random prng = new System.Random();
        int index = prng.Next(enemyPrefabs.Count);

        return GameObject.Instantiate<Enemy>(enemyPrefabs[index]);
    }
}
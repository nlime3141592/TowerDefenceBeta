using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnPointPhase
{
    public float spawnCooltime;
    public List<SpawnRate> spawnRates;

    public Enemy GenerateRandomEnemyOrNull()
    {
        if (spawnRates == null || spawnRates.Count == 0)
            return null;

        System.Random prng = new System.Random();
        int rateIndex = 0;
        int rateSum = spawnRates[0].spawnRate;

        for (int i = 1; i < spawnRates.Count; ++i)
        {
            rateSum += spawnRates[i].spawnRate;

            if (prng.Next(rateSum) < spawnRates[i].spawnRate)
            {
                rateIndex = i;
            }
        }

        SpawnRate selectedSpawnRate = spawnRates[rateIndex];
        return GameObject.Instantiate<Enemy>(selectedSpawnRate.enemyPrefab);
    }
}
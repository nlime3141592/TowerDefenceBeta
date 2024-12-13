using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnPoint
{
    public int spawnPointIndex;

    public List<SpawnPointPhase> phases;

    private float leftSpawnCooltime;

    public void OnUpdate()
    {
        leftSpawnCooltime -= Time.deltaTime;
    }

    public Enemy GenerateEnemyOrNull()
    {
        if (leftSpawnCooltime > 0.0f)
            return null;

        int idxPhase = GameManager.s_gamePhase - 1;

        if (idxPhase >= phases.Count)
            idxPhase = phases.Count - 1;

        SpawnPointPhase phase = phases[idxPhase];
        leftSpawnCooltime = phase.spawnCooltime;

        return phase.GenerateRandomEnemyOrNull();
    }
}
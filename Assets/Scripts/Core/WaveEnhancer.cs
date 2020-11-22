using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEnhancer : MonoBehaviour
{
    [SerializeField]
    float buffMultiplier = .1f;
    [SerializeField]
    EnemyController[] enemies;

    private void Awake()
    {
        Spawner.OnWaveSpawnCompleted += SpawnCompleted;
    }

    private void SpawnCompleted(int wave)
    {
        foreach(EnemyController enemy in enemies)
        {
            // randomly choose to upgrade or miss
            if(Random.Range(0, 2) == 1)
            {
                enemy.enemy.damage *= 1 + buffMultiplier;
                enemy.enemy.health *= 1 + buffMultiplier;
            }

            if(Random.Range(0, 2) == 1)
            {
                enemy.enemy.moveSpeed *= 1 + buffMultiplier;
            }
        }
    }
}

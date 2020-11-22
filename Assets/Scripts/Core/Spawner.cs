using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    float timeBetweenSpawns; // time between enemy spawns
    [SerializeField]
    int maxRandomRange = 10;
    [SerializeField]
    GameObject[] enemyPrefabs;
    [SerializeField]
    BezierCurve[] paths;

    public int SpawnNewWave(int wave)
    {
        int enemyCount = UnityEngine.Random.Range(wave, wave + maxRandomRange + 1);
        StartCoroutine(SpawnWave(timeBetweenSpawns, enemyCount));

        return enemyCount;
    }

    private IEnumerator SpawnWave(float spawnTime, int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            int pathIndex = paths.RandomIndex();
            Instantiate(enemyPrefabs.GetRandomElement(), null).GetComponent<EnemyController>().Init(pathIndex, paths[pathIndex]);

            yield return new WaitForSeconds(spawnTime);
        }
    }
}

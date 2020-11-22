using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Spawner spawner;
    [SerializeField]
    float timeBetweenSpawns = 15;
    [SerializeField]
    int initialMoney;
    [SerializeField]
    CastleController[] castles;

    int currentEnemyCount = 0, killedEnemies = 0;

    public int Wave { get; private set; } = 1;

    public int Money { get; private set; }

    public int TotalHealth
    {
        get {
            float totalHealth = 0;

            foreach (CastleController c in castles)
                totalHealth += c.Health;

            return (int)totalHealth;
        }
    }

    public static event Action<int> OnWaveStarted;
    public static event Action<int> OnWaveEnded;
    public static event Action<int, int> OnGameOver;

    private void Awake()
    {
        Money = initialMoney;

        EnemyController.OnEnemyDied += EnemyDied;
        EnemyController.OnEnemyReachedCastle += EnemyReachedCastle;
    }

    private void Start()
    {
        OnWaveEnded?.Invoke(0);
        StartCoroutine(StartNewWave(timeBetweenSpawns));
    }

    private IEnumerator StartNewWave(float secs)
    {
        yield return new WaitForSeconds(secs);

        currentEnemyCount = spawner.SpawnNewWave(Wave);
        OnWaveStarted?.Invoke(Wave);

        Wave++;
    }

    private void EnemyDied(EnemyController _enemy)
    {
        Money += _enemy.enemy.goldBonus;
        killedEnemies++;

        if((currentEnemyCount--) == 0)
        {
            OnWaveEnded?.Invoke(Wave);
            StartCoroutine(StartNewWave(timeBetweenSpawns));
        } 
    }

    private void EnemyReachedCastle(EnemyController _enemy)
    {
        CastleController c = castles[_enemy.PathwayIndex];

        if(c.TakeDamage(_enemy.enemy.damage))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;

        OnGameOver?.Invoke(Wave, killedEnemies);
    }
}
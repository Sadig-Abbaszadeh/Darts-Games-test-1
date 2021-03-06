﻿using System.Collections;
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
    public static event Action<int> OnMoneyChanged;

    private void Awake()
    {
        Money = initialMoney;

        EnemyController.OnEnemyDied += EnemyDied;
        EnemyController.OnEnemyReachedCastle += EnemyReachedCastle;
    }

    private void Start()
    {
        OnWaveEnded?.Invoke(1);
        ChangeMoney(0);

        StartCoroutine(StartNewWave(timeBetweenSpawns));
    }

    private IEnumerator StartNewWave(float secs)
    {
        Debug.Log("starting");
        yield return new WaitForSeconds(secs);

        currentEnemyCount = spawner.SpawnNewWave(Wave);
        OnWaveStarted?.Invoke(Wave);

        Wave++;
    }

    private void EnemyDied(EnemyController _enemy)
    {
        ChangeMoney(_enemy.enemy.goldBonus);
        killedEnemies++;

        CheckWave();
    }

    private void EnemyReachedCastle(EnemyController _enemy)
    {
        CastleController c = castles[_enemy.PathwayIndex];

        CheckWave();

        if(c.TakeDamage(_enemy.enemy.damage))
        {
            GameOver();
        }
    }

    private void CheckWave()
    {
        currentEnemyCount--;
        if (currentEnemyCount == 0)
        {
            Debug.Log("wave over");
            OnWaveEnded?.Invoke(Wave);
            StartCoroutine(StartNewWave(timeBetweenSpawns));
        }
    }

    private void ChangeMoney(int amount)
    {
        Money += amount;
        OnMoneyChanged?.Invoke(Money);
    }

    private void GameOver()
    {
        Time.timeScale = 0;

        OnGameOver?.Invoke(Wave - 1, killedEnemies);
    }

    public bool CanSpendMoney(int amount)
    {
        if(Money - amount > 0)
        {
            ChangeMoney(-amount);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDestroy()
    {
        EnemyController.OnEnemyDied -= EnemyDied;
        EnemyController.OnEnemyReachedCastle -= EnemyReachedCastle;
    }
}
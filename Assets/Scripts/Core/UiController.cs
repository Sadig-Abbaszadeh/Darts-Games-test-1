﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    TextMeshProUGUI totalHealthText, goldText, currentWaveText, incomingWaveText, deathWaveText, killedEnemiesText;
    [SerializeField]
    GameObject gameOverScreen;

    private void Awake()
    {
        EnemyController.OnEnemyReachedCastle += EnemyReachedCastle;
        EnemyController.OnEnemyDied += EnemyDied;
        GameManager.OnWaveEnded += WaveEnded;
        GameManager.OnWaveStarted += WaveStarted;
        GameManager.OnGameOver += GameOver;
    }

    private void Start()
    {
        EnemyDied(null);
    }

    private void GameOver(int wave, int killedEnemies)
    {
        gameOverScreen.SetActive(true);
        deathWaveText.text = "Final wave: " + wave;
        killedEnemiesText.text = "Killed enemies: " + killedEnemies;
    }

    private void WaveStarted(int wave)
    {
        currentWaveText.gameObject.SetActive(true);
        incomingWaveText.gameObject.SetActive(false);
        currentWaveText.text = "Wave " + wave;
    }

    private void WaveEnded(int wave)
    {
        currentWaveText.gameObject.SetActive(false);
        incomingWaveText.gameObject.SetActive(true);
        incomingWaveText.text = "Wave " + (wave + 1) + " is incoming";
    }

    private void EnemyDied(EnemyController _enemy)
    {
        goldText.text = "" + gameManager.Money;
    }

    private void EnemyReachedCastle(EnemyController _enemy)
    {
        totalHealthText.text = "" + gameManager.TotalHealth;
    }
}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
        GameManager.OnMoneyChanged += MoneyChanged;
        GameManager.OnWaveEnded += WaveEnded;
        GameManager.OnWaveStarted += WaveStarted;
        GameManager.OnGameOver += GameOver;
    }

    private void Start()
    {
        EnemyReachedCastle(null);
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
        Debug.Log("ui");
        currentWaveText.gameObject.SetActive(false);
        incomingWaveText.gameObject.SetActive(true);
        incomingWaveText.text = "Wave " + wave + " is coming soon!";
    }

    private void MoneyChanged(int money)
    {
        goldText.text = "" + money;
    }

    private void EnemyReachedCastle(EnemyController _enemy)
    {
        totalHealthText.text = "" + Mathf.Clamp(gameManager.TotalHealth, 0, 10000);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        EnemyController.OnEnemyReachedCastle -= EnemyReachedCastle;
        GameManager.OnMoneyChanged -= MoneyChanged;
        GameManager.OnWaveEnded -= WaveEnded;
        GameManager.OnWaveStarted -= WaveStarted;
        GameManager.OnGameOver -= GameOver;
    }
}
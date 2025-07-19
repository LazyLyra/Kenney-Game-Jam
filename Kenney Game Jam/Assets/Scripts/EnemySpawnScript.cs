using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public int WaveCounter = 1;
    [SerializeField] public bool WaveOver;
    [SerializeField] private float rateIncreasePerWave;
    [SerializeField] private float baseSpawnRate;
    [SerializeField] private float XtratimeGivenPerRound = 2f;

    private event EventHandler TimeToSpawn;
    private float currentSpawnRate = 0;
    private GameObject enemyWeak;
    private void Start()
    {
        enemyWeak = GameObject.FindGameObjectWithTag("WeakEnemy");
        TimeToSpawn += EnemySpawnScript_TimeToSpawn;

        TimeToSpawn?.Invoke(this, EventArgs.Empty);
    }

    private void Update()
    {


        if (WaveOver) {
            TimeToSpawn?.Invoke(this, EventArgs.Empty);
        }

    }
    private void EnemySpawnScript_TimeToSpawn(object sender, EventArgs e)
    {
        enemyInstantiator(enemyWeak);
        WaveOver = false;
        WaveCounter += 1;
    }

    private float SpawnRateCalculator(int WaveCounter)
    {
        return baseSpawnRate * Mathf.Pow(rateIncreasePerWave, WaveCounter - 1);
    }

    private void enemyInstantiator(GameObject enemy)
    {
        currentSpawnRate = SpawnRateCalculator(WaveCounter);
        for (float timer = 0; timer< 5; timer += Time.deltaTime)
        {
            Instantiate(enemy);
        }
    }

}

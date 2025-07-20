using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class EnemySpawnScript : MonoBehaviour
{
    [Header("Enemy Prefab & Skins")]
    [SerializeField] private List<Sprite> enemySkins;
    [SerializeField] private GameObject enemyWeakPrefab;
    [SerializeField] private GameObject station;

    [Header("Wave Settings")]
    [SerializeField] private int WaveCounter = 1;
    [SerializeField] private float GrowthFactor = 3f;
    [SerializeField] private float baseSpawnRate = 0.5f;   
    [SerializeField] private float baseTimePerRound = 20f;

    [Header("Spawner Settings")]
    [SerializeField] private int MyIndex;

    private float WaveTimer;
    private float Timer;
    private bool WaveOver = false;

    private void Start()
    {
        WaveTimer = baseTimePerRound;
        station = GameObject.FindGameObjectWithTag("Station");
        BeginWave();
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        bool noEnemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
        if (noEnemiesLeft || Timer >= WaveTimer)
        {
            Timer = 0f;
            WaveTimer += 5f;
            WaveOver = true;
            BeginWave();
        }
    }

    private void BeginWave()
    {
        if (!WaveOver && WaveCounter > 1) return;
        WaveOver = false;
        PathManager.Instance.ComputeStationPath(station.transform.position);
        StartCoroutine(SpawnWaveCoroutine());
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        float currentSpawnRate = baseSpawnRate + GrowthFactor * (WaveCounter-1);
        int spawnAttempts = Mathf.CeilToInt(currentSpawnRate * 5f);

        float delay = currentSpawnRate > 0 ? 1f / currentSpawnRate : 0.5f;
        delay = Mathf.Clamp(delay, 0.1f, 1f);

        for (int i = 0; i < spawnAttempts; i++)
        {
            int spawnerIndex = UnityEngine.Random.Range(0, 4);
            if (MyIndex == spawnerIndex)
            {
                Vector3 spawnPos = GetRandomSpawnPosition();
                GameObject go = Instantiate(enemyWeakPrefab, spawnPos, Quaternion.identity);
                if (enemySkins != null && enemySkins.Count > 0)
                {
                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.sprite = enemySkins[UnityEngine.Random.Range(0, enemySkins.Count)];
                    }
                }
            }
            yield return new WaitForSeconds(delay);
        }

        WaveCounter++;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * 2f;
    }
}

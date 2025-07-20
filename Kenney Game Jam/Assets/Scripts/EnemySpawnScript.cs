using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class EnemySpawnScript : MonoBehaviour
{
    [Header("Enemy Prefab & Skins")]
    [SerializeField] private List<Sprite> enemySkins;
    [SerializeField] private RuntimeAnimatorController[] skinAnims;
    [SerializeField] private GameObject enemyWeakPrefab;
    [SerializeField] private GameObject station;

    [Header("Wave Settings")]
    [SerializeField] public int WaveCounter = 0;
    [SerializeField] private float GrowthFactor = 1.5f;
    [SerializeField] private float baseSpawnRate = 0.2f;   
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
        WaveCounter++;
        PathManager.Instance.ComputeStationPath(station.transform.position);
        StartCoroutine(SpawnWaveCoroutine());
    }

    private IEnumerator SpawnWaveCoroutine()
    {
        float currentSpawnRate = baseSpawnRate + GrowthFactor * (WaveCounter - 1);
        int spawnAttempts = Mathf.CeilToInt(currentSpawnRate * 5f);
        spawnAttempts = Mathf.Min(spawnAttempts, 20);

        float delay = currentSpawnRate > 0
            ? 1f / currentSpawnRate
            : 2f;
        delay = Mathf.Clamp(delay, 1f, 2f);

        for (int i = 0; i < spawnAttempts; i++)
        {
            int spawnerIndex = UnityEngine.Random.Range(0, 4);
            if (MyIndex == spawnerIndex)
            {
                Vector3 spawnPos = GetRandomSpawnPosition();
                GameObject go = Instantiate(enemyWeakPrefab, spawnPos, Quaternion.identity);
                if (enemySkins != null && enemySkins.Count > 0)
                {
                    int maxIndex = Mathf.Min(enemySkins.Count, skinAnims.Length);
                    SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
                    Animator animator = go.GetComponent<Animator>();
                    int index = UnityEngine.Random.Range(0, maxIndex);
                    if (sr != null)
                    {
                        sr.sprite = enemySkins[index];
                        animator.runtimeAnimatorController = skinAnims[index];
                        animator.Play("Walk");
                    }
                }
            }
            yield return new WaitForSeconds(delay);
        }

    }


    private Vector3 GetRandomSpawnPosition()
    {
        return transform.position + (Vector3)UnityEngine.Random.insideUnitCircle * 2f;
    }
}

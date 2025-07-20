using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCounterVisual : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] EnemySpawnScript enemySpawnScript;

    private void Start()
    {
        enemySpawnScript = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawnScript>();
    }

    private void Update()
    {
        int waveNum = enemySpawnScript.WaveCounter;
        UpdateWave(waveNum);
    }
    public void UpdateWave(int waveNum)
    {
         uiText.text =  waveNum.ToString();
    }
}

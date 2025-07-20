using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public float EnemyDamage = 2f;
    [SerializeField] private float attackCD = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] float timer;

    [Header("References")]
    [SerializeField] private GameObject station;
    [SerializeField] private GameObject player;
    [SerializeField] private ChargingAreaScript chargingAreaScript;
    [SerializeField] private PlayerHealthScript playerHealthScript;

    private bool isAttacking = false;

    private void Start()
    {
        station = GameObject.FindGameObjectWithTag("Station");
        player = GameObject.FindGameObjectWithTag("Player");
        chargingAreaScript = station.GetComponent<ChargingAreaScript>();
        playerHealthScript = player.GetComponent<PlayerHealthScript>();

        timer = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        
        if (station == null) return;

        float distanceStation = Vector3.Distance(transform.position, station.transform.position);
        if (distanceStation <= attackRange && !isAttacking && timer > attackCD)
        {
            Attack("Station");
            timer = 0f;
        }

        if(player ==null) return;
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance <= attackRange && !isAttacking && timer > attackCD)
        {
            Attack("Player");
            timer = 0f;
        }
    }

    void Attack(string name)
    {
        if (name == "Station")
        {
            chargingAreaScript.TakeDamage(EnemyDamage);
        }else if(name == "Player")
        {
            playerHealthScript.TakeDamage(EnemyDamage);
        }
    }

}

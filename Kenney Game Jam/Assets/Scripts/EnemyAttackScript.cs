using System;
using System.Collections;
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
    [SerializeField] private ChargingAreaScript chargingAreaScript;

    private bool isAttacking = false;

    private void Start()
    {
        station = GameObject.FindGameObjectWithTag("Station");
        chargingAreaScript = GameObject.FindGameObjectWithTag("Station").GetComponent<ChargingAreaScript>();

        timer = 0f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        
        if (station == null) return;

        float distance = Vector3.Distance(transform.position, station.transform.position);
        if (distance <= attackRange && !isAttacking && timer > attackCD)
        {
            Attack();
            timer = 0f;
        }
    }

    void Attack()
    {
        chargingAreaScript.TakeDamage(EnemyDamage);
    }

}

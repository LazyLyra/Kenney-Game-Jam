using System;
using System.Collections;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] public float EnemyDamage = 2f;
    [SerializeField] private float attackCD = 5f;
    [SerializeField] private float attackRange = 2f;

    [Header("References")]
    [SerializeField] private GameObject station;
    [SerializeField] private ChargingAreaScript chargingAreaScript;

    public event EventHandler OnAttack;

    private bool isAttacking = false;

    private void Start()
    {
        station = GameObject.FindGameObjectWithTag("Station");
        chargingAreaScript = GameObject.FindGameObjectWithTag("Station").GetComponent<ChargingAreaScript>();
    }
    private void Update()
    {
        if (station == null) return;

        float distance = Vector3.Distance(transform.position, station.transform.position);
        if (distance <= attackRange && !isAttacking)
        {
            StartCoroutine(AttackLoop());
        }
    }

    private IEnumerator AttackLoop()
    {
        isAttacking = true;

        while (true)
        {
            OnAttack?.Invoke(this, EventArgs.Empty);

            yield return new WaitForSeconds(attackCD);
            isAttacking = false;
        }
    }
}

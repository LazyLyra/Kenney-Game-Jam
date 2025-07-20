using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float MaxHP;
    [SerializeField] private float CurrentHP;
    [SerializeField] private float damage;
    // Start is called before the first frame update
    [Header("References")]
    private int tempholder;

    void Start()
    {
        CurrentHP = MaxHP;
    }

    public void TakeDamage(float damage)
    {
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            Death();
        }
    }
    void Death()
    {
        GameObject.Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("ran in");
            TakeDamage(damage);
        }
    }
}
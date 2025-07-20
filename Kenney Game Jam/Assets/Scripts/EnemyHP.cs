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
    public PlayerShootingScript PSS;

    void Start()
    {
        CurrentHP = MaxHP;

        PSS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShootingScript>();
    }

    public void TakeDamage(float damage)
    {
        if (PSS.Buffed)
        {
            CurrentHP -= 1.5f * damage;
        }
        else
        {
            CurrentHP -= damage;
        }
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

        if (other.CompareTag("BombColliders"))
        {
            TakeDamage(30);
        }
    }

}
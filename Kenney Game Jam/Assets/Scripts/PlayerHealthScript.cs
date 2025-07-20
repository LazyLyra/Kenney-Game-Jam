using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("Health")]
    public float currentHealth;
    public int maxHealth;

    [Header("References")]
    public PlayerSFXManager sfx;

    [Header("Buffs")]
    public bool Shielded;
    [SerializeField] float shieldTime;
    [SerializeField] float timer;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSFXManager>();

        currentHealth = maxHealth;
        timer = 0f;
        Shielded = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shieldTime)
        {
            Shielded = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (Shielded)
        {
            currentHealth -= 0.7f * damage;
        }
        else
        {
            currentHealth -= damage;
        }
        
        

        if (currentHealth <= 0)
        {
            //die :(
        }
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
        Mathf.Clamp(currentHealth, 0, maxHealth);

        sfx.PlaySound(2);
    }

    public void StartShield()
    {
        timer = 0f;
        Shielded = true;

        sfx.PlaySound(2);
    }
}

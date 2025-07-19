using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("Health")]
    public int currentHealth;
    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            //die :(
        }
    }

    public void Heal(int healing)
    {
        currentHealth += healing;
        Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}

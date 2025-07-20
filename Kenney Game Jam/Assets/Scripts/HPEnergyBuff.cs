using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEnergyBuff : MonoBehaviour
{
    public int HealAmount;
    public float EnergyAmount;

    public PlayerHealthScript PHS;
    public PlayerEnergyScript PES;
    public BoxCollider2D BC;

    // Start is called before the first frame update
    void Start()
    {
        BC = GetComponent<BoxCollider2D>();
        PHS = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealthScript>();
        PES = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            PHS.Heal(HealAmount);

            PES.GainEnergy(EnergyAmount);
        }

        GameObject.Destroy(gameObject);
    }
}

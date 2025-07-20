using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingAreaScript : MonoBehaviour
{
    public PlayerEnergyScript PES;
    public CircleCollider2D CC;
    public GameObject Beam;
    [SerializeField] float cooldownTime;
    [SerializeField] float timer;
    [SerializeField] float energyGain;
    [SerializeField] float chargeRadius;

    [SerializeField] float beamCD;
    [SerializeField] float beamTimer;

    public float currentHP;
    public float maxHP;
    // Start is called before the first frame update
    void Start()
    {
        PES = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyScript>();
        CC = GetComponent<CircleCollider2D>();

        timer = 0f;
        beamTimer = 0f;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        beamTimer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, PES.transform.position);

        if (beamTimer > beamCD && distance < chargeRadius)
        {
            Instantiate(Beam);

            beamTimer = 0f;
        }
        if (timer > cooldownTime && (distance < chargeRadius))
        {
            PES.GainEnergy(energyGain);

            timer = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP = -damage;

        Mathf.Clamp(currentHP, 0, maxHP);
    }
}

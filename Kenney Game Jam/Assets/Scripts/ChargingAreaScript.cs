using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingAreaScript : MonoBehaviour
{
    public PlayerEnergyScript PES;
    [SerializeField] float cooldownTime;
    [SerializeField] float timer;
    [SerializeField] float energyGain;
    [SerializeField] float chargeRadius;
    // Start is called before the first frame update
    void Start()
    {
        PES = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyScript>();

        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, PES.transform.position);

        if (timer > cooldownTime && (distance < chargeRadius))
        {
            PES.GainEnergy(energyGain);

            timer = 0f;
        }
    }
}

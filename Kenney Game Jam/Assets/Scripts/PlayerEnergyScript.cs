using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyScript : MonoBehaviour
{
    public float currentEnergy;
    public float maxEnergy;
    [SerializeField] float energyDecay;
    
    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        currentEnergy -= energyDecay * Time.deltaTime;
    }

    public void UseEnergy(float usage)
    {
        currentEnergy -= usage;

        if (currentEnergy <= 0)
        {
            //die
        }
    }

    public void GainEnergy(float gain)
    {
        currentEnergy += gain;

        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }
        
    }
}

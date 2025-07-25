using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnergyScript : MonoBehaviour
{
    public float currentEnergy;
    public float maxEnergy;
    [SerializeField] float energyDecay;

    public PlayerSFXManager sfx;
    
    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSFXManager>();

        currentEnergy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        Decay();
    }

    public void UseEnergy(float usage)
    {
        currentEnergy -= usage;

        if (currentEnergy <= 0)
        {
            //die
            StartCoroutine(Die());
        }
    }

    public void Decay()
    {
        currentEnergy -= energyDecay * Time.deltaTime;

        if (currentEnergy <= 0)
        {
            //die
            StartCoroutine(Die());
        }
    }

    public void GainEnergy(float gain)
    {
        currentEnergy += gain;

        if (currentEnergy > maxEnergy)
        {
            currentEnergy = maxEnergy;
        }

        sfx.PlaySound(1);
        
    }

    private IEnumerator Die()
    {
        sfx.PlaySound(4);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(5);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour
{
    public PlayerEnergyScript PES;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        PES = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEnergyScript>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = (PES.currentEnergy / PES.maxEnergy);
    }
}
